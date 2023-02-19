using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingInput : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    private float distanceATerre = 0.5f;

    private bool estATerre;
    
    private void Awake() => playerRigidbody = GetComponent<Rigidbody>();


    private void Jump()
    {
        if (estATerre)
            playerRigidbody.AddForce(6f * Vector3.up, ForceMode.Impulse);
    }


    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distanceATerre + 0.1f))
            estATerre = true;
        else
            estATerre = false;
        if (enCoursDeSaut)
        {
            Jump();
            enCoursDeSaut = false;
        }
    }

    private bool enCoursDeSaut = false;

    private void OnJump()
    {
        enCoursDeSaut = true;
    }

}
