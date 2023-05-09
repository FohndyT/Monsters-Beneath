// Jeremy Legault

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
    public Vector3 skewedMovement;
    Vector3 skewedDirection;
    const float moveVelo = 9f;
    public float dashVelo = 40f;
    float dashDuration = 0.4f;
    float dashCooldown = 0.1f;
    bool isDashing;
    private bool canDash = true;
    #endregion
    #region Looking
    public bool zTargeting;
    Collider[] targets = new Collider[0];
    public Transform zTarget { get; private set; }
    #endregion
    #region Attacking
    [SerializeField] private GameObject Sword;
    float attackCooldown = 0.5f;
    public bool acquiredSword;
    public bool canAttack = true;
    #endregion
    #region Items
    [SerializeField] GameObject Fouet;
    [SerializeField] GameObject Projectile;
    [SerializeField] GameObject CrystalLight;
    [SerializeField] GameObject CrystalLaser;
    public GameObject[] Items { get; private set; }
    public int selectedItem { get; private set; } = -1;
    int currentItem = -1;
    public bool canUseItem = true;
    private bool isUsingLight = false;
    #endregion
    #region Autres
    bool camLock = false;
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
                if (!zTargeting)
                {
                    playbody.velocity = camBehave.eagleView ? new Vector3(skewedMovement.x * moveVelo, playbody.velocity.y, skewedMovement.z * moveVelo) :
                                                              Vector3.up * playbody.velocity.y + rawMovement.x * moveVelo * transitionCam.sideviewWorldDirection;
                }
                else
                    playbody.velocity = moveVelo * rawMovement.z * transform.forward.normalized + moveVelo * rawMovement.x * transform.right.normalized + playbody.velocity.y * Vector3.up;
                if (zTargeting)
                    transform.LookAt(new Vector3(zTarget.position.x, transform.position.y, zTarget.position.z));
                else if (skewedDirection != Vector3.zero && !camLock)
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
            StartCoroutine(OrientationLock(playerInput.actions["Orientation Lock"]));
        else
        {
            float[] targetDistances = new float[targets.Length];
            for (int i = 0; i < targets.Length; i++)
                targetDistances[i] = Vector3.Distance(targets[i].transform.position, transform.position);
            Array.Sort(targetDistances, targets);

            zTargeting = true;
            zTarget = targets[0].transform;
            StartCoroutine(Targeting(playerInput.actions["Orientation Lock"]));
        }
    }
    IEnumerator Targeting(InputAction lockAction)
    {
        zTarget.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        while (lockAction.inProgress)
        {
            if (Vector3.Distance(zTarget.position, transform.position) > 12f && zTarget != null)
                break;
            yield return null;
        }
        zTarget.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
        zTargeting = false;
        StopCoroutine(Targeting(lockAction));
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
    { if (acquiredSword && canAttack) { StartCoroutine(Attack()); } }
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
        if (player.itemsAcquired.Length != 0 || selectedItem != -1)
        {
            if (currentItem < player.itemsAcquired.Length - 1)
                selectedItem = player.itemsAcquired[++currentItem];
            else
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