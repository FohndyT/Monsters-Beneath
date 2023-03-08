using UnityEngine;
using UnityEngine.InputSystem;

public class InputsManager : MonoBehaviour
{
    #region Attributes
    Rigidbody playbody;
    MeshFilter meshFilth;
    PlayerInput playerInput;
    Vector3 rawMovement;
    Vector3 skewedMovement;
    Vector3 skewedDirection;
    bool camLock = false;
    const float jumpForce = 15000f;     // Mass de 30
    const float gravityForce = 600f;    // Drag de 0
    public bool isGrounded = false;     // Angular drag de 0.05
    public RaycastHit rayHit;
    #endregion 

    void Start()
    {
        playbody = GetComponent<Rigidbody>();
        meshFilth = GetComponent<MeshFilter>();
        playerInput = GetComponent<PlayerInput>();
        playbody.freezeRotation = true;
    }
    private void Update()
    {
        CheckIfGrounded();
        TransformPlayer();
    }
    void CheckIfGrounded()
    {
        if (Physics.Raycast(transform.position, -transform.up, out rayHit, meshFilth.mesh.bounds.extents.y + 0.1f))     // ray hit a l'origine du monde? ca fait un double saut?
            isGrounded = true;
    }
    void TransformPlayer()
    {
        playbody.velocity = new Vector3(skewedMovement.x * 6, playbody.velocity.y, skewedMovement.z * 6);
        playbody.AddForce(new Vector3(0, -gravityForce, 0));
        if (skewedDirection != Vector3.zero && !camLock)
            transform.forward = Vector3.Lerp(transform.forward, skewedDirection, Time.deltaTime * 35);  //  Slerp pour rotation de 180 non abruptes, mais rotation trop unnatural. Enleve le focus.
        //transform.rotation = Quaternion.LookRotation(playbody.velocity, transform.up);    //  Dangerous but fun. Could be applied as a "confused player" state
    }

    void OnMove(InputValue value)
    {
        rawMovement = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
        skewedMovement = Quaternion.AngleAxis(45, Vector3.up) * rawMovement;
        skewedDirection = Quaternion.AngleAxis(-45, Vector3.up) * rawMovement;  // Mouvement avec souris est HYPER plus vite, va falloir limiter le range... detecter le medium?
    }
    void OnOrientationLock(InputValue value)
    {
        //camLock = true; //  Puisque detect juste premire frame, le cam lock perdure. Must find a way de faire que les actions se font a chaque frame, this way le transform en haut vas pouvoir etre deplace au OnMove()
    }
    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            playbody.velocity.Set(playbody.velocity.x, 0, playbody.velocity.z);
            playbody.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
        }
    }
    void OnAttack(InputValue value)
    {
        /* (Commentaires personnels)
         * Instantiate boxcollider (with timer? stopwatch? difference de Time.time?)
         * Call Debug ColliderBox ou juste laisser accez aux dynamically instanciated colliders qu'ils puisse les referencer dans son script
         * Call Method that send message to enemy that was hit
         * Repeat for second attack phase collider (with DebugGodMode affichage)
         */
    }
    void OnAttackCharged(InputValue value) { }
    void OnItem(InputValue value) { }
    void OnItemUseSwap(InputValue value) { }
    void OnItemSelect(InputValue value) { }
    void OnOpenMenu(InputValue value)
    {
        //if (!inMenu)
        //{
        //    playerInput.SwitchCurrentActionMap("Menu");
        //    Debug.Log(playerInput.currentActionMap);
        //    inMenu = true;
        //}
    }

    //  Menu Inputs
    void OnMoveCursor(InputValue inputValue) { }
    void OnSelect(InputValue inputvalue) { }
    void OnBack(InputValue inputValue) { }
}
