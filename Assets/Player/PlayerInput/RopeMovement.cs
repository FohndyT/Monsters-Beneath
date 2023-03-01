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

    private void Start()
    {
        mainRope = GameObject.FindWithTag("MainRope");
        selfRigidbody = GetComponent<Rigidbody>();
        ropeRigidbody = mainRope.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (onRope == true)
        {
            transform.position = new Vector3(mainRope.transform.position.x,
                                           mainRope.transform.position.y - characterPositionY,
                                             mainRope.transform.position.z);

            if (Input.GetKey("space"))
            {
                onRope = false;
                selfRigidbody.velocity = new Vector3(ropeRigidbody.velocity.x, 
                                                   ropeRigidbody.velocity.y + 3, 
                                                     ropeRigidbody.velocity.z);
            }
        }
    }

    private void OnTriggerEnter(Collider mainRope)
    {
        if (mainRope.CompareTag("MainRope"))
        {
            characterPositionY = mainRope.transform.position.y - transform.position.y;
            //characterPositionX = transform.position.x - mainRope.transform.position.x;
            onRope = true;
            Debug.Log("Collision MainRope Detected");
            ropeRigidbody.AddForce(new Vector3(selfRigidbody.velocity.x * 35,0,selfRigidbody.velocity.z * 35));
        }
    }
}
