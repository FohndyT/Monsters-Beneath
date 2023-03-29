using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsManager : MonoBehaviour
{
    #region Components
    Rigidbody playbody;
    BoxCollider boxColle;
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
    #endregion
    #region Moving
    Vector3 rawMovement;
    protected Vector3 skewedMovement;
    Vector3 skewedDirection;
    const float appliedVelo = 8f;
    private float tempsEcoule = 0;
    public float dashForce = 20;
    private bool canDash = true;
    #endregion
    #region Attacking
    [SerializeField] private GameObject Sword;
    #endregion
    #region Items
    [SerializeField] private GameObject Fouet;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private GameObject PowerGlove;
    [SerializeField] private GameObject CrystalLight;
    private GameObject[] Items;
    private int itemIndex = 0;
    private float cooldownTimer = 0f;
    public bool canAttack = true;
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
        boxColle = GetComponent<BoxCollider>();
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
        ActionsCheck();
    }

    void CheckIfGrounded()
    { estATerre = Physics.Raycast(transform.position, -transform.up, out rayHit, (boxColle.size.y * 0.5f) + 0.05f); }
    void MovePlayer()
    {
        if (!camPathTraveler.isActiveAndEnabled)
        {
            playbody.velocity = camBehave.eagleView ? new Vector3(skewedMovement.x * appliedVelo, playbody.velocity.y, skewedMovement.z * appliedVelo) :
                                                      Vector3.up * playbody.velocity.y + rawMovement.x * appliedVelo * transitionCam.sideviewdirection;
            playbody.AddForce(new Vector3(0, -gravityForce, 0));
            if (skewedDirection != Vector3.zero && !camLock)
            {
                transform.forward = camBehave.eagleView ? Vector3.Lerp(transform.forward, skewedDirection, Time.deltaTime * 35) :
                                                          rawMovement.x * transitionCam.sideviewdirection + rawMovement.z * Vector3.Cross(transitionCam.sideviewdirection, transitionCam.sideviewUpVector);
            }
        }
        //transform.rotation = Quaternion.LookRotation(playbody.velocity, transform.up);    //  Dangerous but fun. Could be applied as a "confused player" state
    }
    void ActionsCheck()
    {
        if (tempsEcoule > 3f)
        {
            canDash = true;
            tempsEcoule = 0;
        }
        tempsEcoule += Time.deltaTime;

        if (!canAttack)
        {
            if (cooldownTimer < 0.75f)
                cooldownTimer += Time.deltaTime;
            else
            {
                canAttack = true;
                cooldownTimer = 0;
            }
        }
    }

    void OnMove(InputValue value)
    {
        rawMovement = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
        skewedMovement = Quaternion.AngleAxis(45, Vector3.up) * rawMovement;
        skewedDirection = Quaternion.AngleAxis(35, Vector3.up) * rawMovement;  // Mouvement avec souris est HYPER plus vite, va falloir limiter le range... detecter le medium?
    }
    private void OnDash()
    {
        if (canDash)
        {
            playbody.AddForce(skewedDirection.x * dashForce, 0, skewedDirection.z * dashForce, ForceMode.Impulse);
            canDash = false;
        }
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
    {
        Instantiate(Sword, handPos);
        /* (Commentaires personnels)
         * Instantiate boxcollider (with timer? stopwatch? coroutine?)
         * Call Method that send message to enemy that was hit
         * Repeat for second attack phase collider (with Devtools affichage)
         */
    }
    void OnAttackCharged(InputValue value) { }
    void OnItem(InputValue value)
    {
        if (canAttack)
        {
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
        canAttack = false;
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
