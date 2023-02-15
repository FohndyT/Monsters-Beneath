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
    public float dashVitesse;
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
    public float directionHoriontale = 1;
    public float directionVerticale = 1;


    private void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.x < 0)
        {
            directionHoriontale = -1;
        }
        else
        {
            directionHoriontale = 1;
        }

        if (moveInput.y < 0)
        {
            directionVerticale = -1;
        }
        else
        {
            directionVerticale = 1;
        }
        gameObject.transform.Rotate(0,90*directionHoriontale+90*directionVerticale,0,Space.World);
    }

    private void OnDash()
    {
        if(CanDash)
        {
            if(moveInput.x != 0) 
                playerRigidbody.AddForce(dashDistance*directionHoriontale,0,0,ForceMode.Impulse);
            else if(moveInput.y!= 0) 
                playerRigidbody.AddForce(0,0,dashDistance*directionVerticale,ForceMode.Impulse);
            CanDash = false;
        }
    }
}
