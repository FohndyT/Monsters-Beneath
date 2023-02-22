using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInput : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
    
    
    public float vitesse = 2f;
    public float dashDistance;
    private bool CanDash = true;
    

    private float tempsÉcoulé = 0;
    private void FixedUpdate()
    {
        playerRigidbody.AddForce(moveInput.x*vitesse,0,moveInput.y*vitesse,ForceMode.Force);
        if (tempsÉcoulé > 3f)
        {
            tempsÉcoulé = 0;
            CanDash = true;
        }   
        tempsÉcoulé += Time.deltaTime;
    }

    private Vector2 moveInput;


    private void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        moveInput.Normalize();
        Vector3 moveInput3d = new Vector3(moveInput.x, 0, moveInput.y);
        if (moveInput3d != Vector3.zero)
            transform.forward = moveInput3d;
    }

    private void OnDash()
    {
        if(CanDash)
        {
            if (moveInput == Vector2.zero)
                return;
            else if (moveInput.x != 0)
            {
                playerRigidbody.AddForce(moveInput.x * dashDistance, 0, 0, ForceMode.Impulse);
                CanDash = false;
            }
            else if (moveInput.y != 0)
            {
                playerRigidbody.AddForce(0, 0, moveInput.y * dashDistance, ForceMode.Impulse);
                CanDash = false;
            }
        }
    }
}
