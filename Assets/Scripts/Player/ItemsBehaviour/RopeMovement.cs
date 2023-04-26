using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class RopeMovement : MonoBehaviour
{
    private bool onRope = false;

    private float characterPositionY;

    GameObject mainRope;
    private Rigidbody selfRigidbody;
    private Rigidbody ropeRigidbody;

    private float playerSpeed;

    private void Start()
    {
        selfRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (onRope == true)
        {
            transform.position = new Vector3(mainRope.transform.position.x,
                                           mainRope.transform.position.y - characterPositionY,
                                             mainRope.transform.position.z);
            
            if (Input.GetKey("w"))
            {
                ropeRigidbody.velocity = new Vector3(0, 0, playerSpeed);
            }
            
            if (Input.GetKey("s"))
            {
                ropeRigidbody.velocity = new Vector3(0, 0, -playerSpeed);
            }

            if (Input.GetKey("space"))
            {
                onRope = false;
                selfRigidbody.velocity = new Vector3(0, ropeRigidbody.velocity.y + 2, ropeRigidbody.velocity.z);
            }
        }
    }

    private void OnTriggerEnter(Collider rope)
    {
        if (rope.CompareTag("MainRope") || rope.CompareTag("MainRope2"))
        {
            onRope = true;
            
            playerSpeed = selfRigidbody.velocity.z;
            characterPositionY = rope.transform.position.y - transform.position.y;
            
            Debug.Log("Collision Rope Detected");
            
            if (rope.CompareTag("MainRope"))
            {
                mainRope = GameObject.FindWithTag("MainRope");
            }

            if (rope.CompareTag("MainRope2"))
            {
                mainRope = GameObject.FindWithTag("MainRope2");
            }
            
            ropeRigidbody = mainRope.GetComponent<Rigidbody>();
            ropeRigidbody.AddForce(new Vector3(selfRigidbody.velocity.x * 35,0,selfRigidbody.velocity.z * 35));
        }
    }
}
