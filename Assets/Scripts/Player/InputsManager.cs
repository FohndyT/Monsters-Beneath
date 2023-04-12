using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsManager : MonoBehaviour
{
    #region Components
    Rigidbody playbody;
    PlayerInput playerInput;
    CameraBehaviour camBehave;
    CurveTraveler camPathTraveler;
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
    [SerializeField] private GameObject PowerGlove;
    [SerializeField] private GameObject CrystalLight;
    private GameObject[] Items;
    private int itemIndex = 0;
    public static bool usingLight = false;
    #endregion
    #region Autres
    Transform handPos;
    bool camLock = false;
    bool inMenu = true;
    #endregion

    void Start()
    {
        playbody = GetComponent<Rigidbody>();
        rayHitMax = (GetComponent<BoxCollider>().size.y * 0.5f) + 0.05f;
        playerInput = GetComponent<PlayerInput>();
        playbody.freezeRotation = true;
        handPos = GameObject.Find("PlayerHandPos").transform;
        Items = new[] { Fouet, Projectile, PowerGlove, CrystalLight };
        Camera camu = Camera.main;
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
                playbody.velocity = camBehave.eagleView ? new Vector3(skewedMovement.x * moveVelo, playbody.velocity.y, skewedMovement.z * moveVelo) :
                                                          Vector3.up * playbody.velocity.y + rawMovement.x * moveVelo * transitionCam.sideviewdirection;
                if (skewedDirection != Vector3.zero && !camLock)
                {
                    transform.forward = camBehave.eagleView ? Vector3.Lerp(transform.forward, skewedDirection, Time.deltaTime * 35) :
                                                              rawMovement.x * transitionCam.sideviewdirection + rawMovement.z * Vector3.Cross(transitionCam.sideviewdirection, transitionCam.sideviewUpVector);
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
    {
        camLock = true;     // Only called once. Trouver comment appeler a repetition lorsque le button est held.
        Invoke("OrientationUnlock", 1f);
    }
    void OrientationUnlock()
    {
        camLock = false;
    }
    private void OnDash()
    { if (canDash) { StartCoroutine(Dash()); } }
    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        playbody.velocity = camBehave.eagleView ? new Vector3(skewedMovement.x * dashVelo, 0, skewedMovement.z * dashVelo) :
                                          Vector3.up * playbody.velocity.y + rawMovement.x * dashVelo * transitionCam.sideviewdirection;
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
        Instantiate(Sword, handPos);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        StopCoroutine(Attack());
    }
    void OnAttackCharged(InputValue value) { }
    void OnItem(InputValue value)
    {
        if (canAttack)
        {
            canAttack = false;
            if (itemIndex == 3)
            {
                if (usingLight)
                {
                    usingLight = false;
                    return;
                }
                Instantiate(CrystalLight, handPos);
                usingLight = true;
                return;
            }
            if (itemIndex == 1)
            {
                Instantiate(Projectile, handPos);
                handPos.transform.DetachChildren();
            }
            else
                Instantiate(Items[itemIndex], handPos);
        }
    }
    void OnItemUseSwap(InputValue value) { }
    void OnItemSelect(InputValue value)
    {
        if (usingLight)
            return;
        itemIndex = itemIndex < Items.Length - 1 ? +1 : 0;
    }
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
