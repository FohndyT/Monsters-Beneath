using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputsManager : MonoBehaviour
{
    #region Components
    Rigidbody playbody;
    PlayerInput playerInput;
    CameraBehaviour camBehave;
    CurveTraveler camPathTraveler;
    Transform handLeftPos;
    Transform handRightPos;
    DevTools devtools;
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
    public GameObject[] Items;
    public int itemIndex = 0;
    public bool canUseItem = true;
    #endregion
    #region Autres
    bool camLock = false;
    bool inMenu = true;
    #endregion

    void Awake()
    {
        playbody = GetComponent<Rigidbody>();
        rayHitMax = (GetComponent<BoxCollider>().size.y * 0.5f) + 0.05f;
        playerInput = GetComponent<PlayerInput>();
        playbody.freezeRotation = true;
        handLeftPos = GameObject.Find("PlayerLeftHandPos").transform;
        handRightPos = GameObject.Find("PlayerRightHandPos").transform;
        Items = new[] { Fouet, Projectile, CrystalLight, CrystalLaser };
        Camera camu = Camera.main;
        camBehave = camu.GetComponent<CameraBehaviour>();
        camPathTraveler = camu.GetComponent<CurveTraveler>();
        devtools = GetComponent<DevTools>();
    }
    private void Update()
    {
        CheckIfGrounded();
        MovePlayer();
        //Debug.Log(canUseItem.ToString() + "  " + Items[itemIndex].ToString());
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
        skewedDirection = Quaternion.AngleAxis(35, Vector3.up) * rawMovement;  // Mouvement avec souris est HYPER plus vite, va falloir limiter le range... detecter le medium?
    }
    void OnOrientationLock(InputValue value)
    { StartCoroutine(OrientationLock(playerInput.actions["Orientation Lock"])); }
    IEnumerator OrientationLock(InputAction lockAction)
    {
        while (lockAction.inProgress)
        {
            camLock = true;
            yield return null;
        }
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
        if (canUseItem)
        {
            canUseItem = false;
            Instantiate(Items[itemIndex], handLeftPos);
            if (itemIndex == 1)
                handLeftPos.transform.DetachChildren();
        }
    }
    void OnItemUseSwap(InputValue value) { }    // Relics of a past idea
    void OnItemSelect(InputValue value)
    { itemIndex = itemIndex < Items.Length - 1 ? itemIndex + 1 : 0; }
    void OnOpenMenu(InputValue value)
    {
        if (inMenu) { playerInput.SwitchCurrentActionMap("Gameplay"); }
        else { playerInput.SwitchCurrentActionMap("Menu"); }
        Debug.Log(playerInput.currentActionMap);
        inMenu = !inMenu;
    }

    //  Menu Inputs
    void OnMoveCursor(InputValue inputValue) { }
    void OnSelect(InputValue inputvalue) { }
    void OnBack(InputValue inputValue) { }
}
