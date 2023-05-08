using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputsManager : MonoBehaviour
{
    #region Components
    Rigidbody playbody;
    PlayerInput playerInput;
    Player player;
    Camera camu;
    CameraBehaviour camBehave;
    CurveTraveler camPathTraveler;
    Transform handLeftPos;
    Transform handRightPos;
    public Transition2D3D transitionCam { get; set; }
    #endregion
    #region Jumping
    const float jumpForce = 16000f;     // Mass de 30
    const float gravityForce = 600f;    // Drag de 0
    public bool estATerre = false;     // Angular drag de 0.05
    public RaycastHit rayHit;
    public float rayHitMax { get; private set; }
    #endregion
    #region Moving
    Vector3 rawMovement;
    protected Vector3 skewedMovement;
    Vector3 skewedDirection;
    const float moveVelo = 9f;
    public float dashVelo = 40f;
    float dashDuration = 0.4f;
    float dashCooldown = 0.1f;
    bool isDashing;
    private bool canDash = true;
    #endregion
    #region Looking
    bool zTargeting;
    Collider[] targets = new Collider[0];
    int targetIndex = -1;
    Transform target;
    Vector3 posWhenLock = Vector3.zero;
    GameObject crosshair;
    #endregion
    #region Attacking
    [SerializeField] private GameObject Sword;
    float attackCooldown = 0.5f;
    public bool canAttack = true;
    #endregion
    #region Items
    [SerializeField] private GameObject Fouet;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private GameObject CrystalLight;
    [SerializeField] private GameObject CrystalLaser;
    public GameObject[] Items { get; private set; }
    public int selectedItem { get; private set; } = -1;
    int currentItem = -1;
    public bool canUseItem = true;
    private bool isUsingLight = false;
    #endregion
    #region Autres
    bool camLock = false;
    bool inMenu = true;
    public GameObject PauseMenuCanvas;
    #endregion

    void Awake()
    {
        playbody = GetComponent<Rigidbody>();
        rayHitMax = (GetComponent<BoxCollider>().size.y * 0.5f) + 0.05f;
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
        playbody.freezeRotation = true;
        handLeftPos = GameObject.Find("PlayerLeftHandPos").transform;
        handRightPos = GameObject.Find("PlayerRightHandPos").transform;
        Items = new[] { Fouet, Projectile, CrystalLight, CrystalLaser };
        camu = Camera.main;
        camBehave = camu.GetComponent<CameraBehaviour>();
        camPathTraveler = camu.GetComponent<CurveTraveler>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }
    private void Update()
    {
        CheckIfGrounded();
        MovePlayer();
    }

    void CheckIfGrounded()
    { estATerre = Physics.Raycast(transform.position, -transform.up, out rayHit, rayHitMax); }
    void MovePlayer()
    {
        if (!camPathTraveler.isActiveAndEnabled)
        {
            if (!isDashing)
            {
                playbody.velocity = camBehave.eagleView ? new Vector3(skewedMovement.x * moveVelo, playbody.velocity.y, skewedMovement.z * moveVelo) :
                                                          Vector3.up * playbody.velocity.y + rawMovement.x * moveVelo * transitionCam.sideviewWorldDirection;
                if (skewedDirection != Vector3.zero && !camLock)
                {
                    transform.forward = camBehave.eagleView ? Vector3.Lerp(transform.forward, skewedDirection, Time.deltaTime * 35) :
                                                              rawMovement.x * transitionCam.sideviewWorldDirection + rawMovement.z * Vector3.Cross(transitionCam.sideviewWorldDirection, transitionCam.sideviewWorldUpVector);
                }
            }
            playbody.AddForce(new Vector3(0, -gravityForce, 0));
        }
    }
    void OnMove(InputValue value)
    {
        rawMovement = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
        skewedMovement = Quaternion.AngleAxis(45, Vector3.up) * rawMovement;
        skewedDirection = Quaternion.AngleAxis(35, Vector3.up) * rawMovement;  // Mouvement avec souris est HYPER plus vite, used as Debug.
    }
    void OnOrientationLock(InputValue value)
    {
        targets = Physics.OverlapBox(4.5f * transform.forward + transform.position, new(5, 2, 5), transform.rotation);
        targets = targets.Where(x => x.transform.CompareTag("Enemy")).Where(x => x.GetType().Equals(typeof(BoxCollider))).ToArray();

        if (targets == null || targets.Length < 1)      // Pas d'ennemi en vue
        {
            targetIndex = -1;
            StartCoroutine(OrientationLock(playerInput.actions["Orientation Lock"]));
        }
        else
        {
            if (Vector3.Distance(posWhenLock, transform.position) > 0.001f)     // Nouveau Z Target
            {
                posWhenLock = transform.position;
                targetIndex = 0;

                float[] targetDistances = new float[targets.Length];
                for (int i = 0; i < targets.Length; i++)
                    targetDistances[i] = Vector3.Distance(targets[i].transform.position, transform.position);
                Array.Sort(targetDistances, targets);
            }
            else        // N'a pas boug� depuis le dernier Z Target
                targetIndex++;

            zTargeting = true;
            target = targets[targetIndex].transform;
            //crosshair.gameObject.SetActive(true);
            StartCoroutine(Targeting(playerInput.actions["Orientation Lock"]));


            // TODO : adapter OnMove en consequence de zTargeting
        }
    }
    IEnumerator Targeting(InputAction lockAction)
    {
        GameObject temp = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));         // (temp)
        temp.transform.localScale = 1.8f * Vector3.one;                                                     // (temp)

        while (lockAction.inProgress)
        {

            temp.transform.SetPositionAndRotation(target.position, Quaternion.Euler(new Vector3(Mathf.Sin(Time.time), 0, Mathf.Cos(Time.time))));
            //crosshair.transform.SetPositionAndRotation(camu.WorldToScreenPoint(target.position),
                                                       //Quaternion.Euler(new Vector3(Mathf.Sin(Time.time), 0, Mathf.Cos(Time.time))));
            yield return null;
        }
        DestroyImmediate(temp, true);                                                                      // (temp)
        //crosshair.gameObject.SetActive(false);
        zTargeting = false;
    }
    IEnumerator OrientationLock(InputAction lockAction)
    {
        camLock = true;
        while (lockAction.inProgress)
            yield return null;
        camLock = false;
        StopCoroutine(OrientationLock(lockAction));
    }
    private void OnDash()
    { if (canDash) { StartCoroutine(Dash()); } }
    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        playbody.velocity = camBehave.eagleView ? new Vector3(skewedMovement.x * dashVelo, 0, skewedMovement.z * dashVelo) :
                                          Vector3.up * playbody.velocity.y + rawMovement.x * dashVelo * transitionCam.sideviewWorldDirection;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        StopCoroutine(Dash());
    }
    void OnJump(InputValue value)
    {
        if (estATerre)
        {
            playbody.velocity.Set(playbody.velocity.x, 0, playbody.velocity.z);
            playbody.AddForce(jumpForce * Vector3.up);
            estATerre = false;
        }
    }
    void OnAttack(InputValue value)
    { if (canAttack) { StartCoroutine(Attack()); } }
    IEnumerator Attack()
    {
        canAttack = false;
        Instantiate(Sword, handRightPos);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        StopCoroutine(Attack());
    }
    void OnAttackCharged(InputValue value) { }
    void OnItem(InputValue value)
    {
        if (canUseItem && selectedItem != -1)
        {
            if (selectedItem is 2 or 3)
            {
                if (isUsingLight)
                {
                    isUsingLight = false;
                    Destroy(GameObject.FindGameObjectWithTag("Light"));
                    return;
                }
                isUsingLight = true;
            }
            canUseItem = false;
            Instantiate(Items[selectedItem], handLeftPos);
            if (selectedItem == 1)
                handLeftPos.transform.DetachChildren();
        }
    }
    void OnItemUseSwap(InputValue value) { }    // Relics of a past idea
    void OnItemSelect(InputValue value)
    {
        if (player.itemsAcquired != null)
        {
            if (currentItem < player.itemsAcquired.Length - 1)
                selectedItem = player.itemsAcquired[++currentItem];
            else if (player.itemsAcquired != null)
            {
                selectedItem = player.itemsAcquired[0];             // selectedItem = index de l'item en cours dans Items        PROBLEMO : even if selectedItem is changed correctly, le laser ne veut pas Destroy, et les autres items afterwards ne s'Instantiatent pas
                currentItem = 0;                                    // currentItem = index actuel de player.itemsAcquired
            }
            //selectedItem = currentItem < player.itemsAcquired.Length - 1 ? player.itemsAcquired[++currentItem] : player.itemsAcquired[0];
        }
    }
    void OnOpenMenu(InputValue value)
    {
        playerInput.SwitchCurrentActionMap("Menu");
        Debug.Log(playerInput.currentActionMap);

        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;

    }

    //  Menu Inputs
    void OnMoveCursor(InputValue inputValue) { }
    void OnSelect(InputValue inputvalue) { }

    void OnBack(InputValue inputValue)
    {
        playerInput.SwitchCurrentActionMap("Gameplay");
        Debug.Log(playerInput.currentActionMap);

        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
