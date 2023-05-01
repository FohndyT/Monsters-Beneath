using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Rigidbody rb;
    GameObject parentObj;
    public float timeSpent = 0f;
    private float maxTime = 5f;

    private void Awake()
    {
        parentObj = transform.parent.gameObject;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward*50f, ForceMode.Impulse);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 8) 
           Destroy(parentObj);
    }
*/
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contacts = collision.contacts;
        ContactPoint firstContact = contacts[0];
        Collider thisCollider = firstContact.thisCollider;
        if (firstContact.otherCollider.gameObject.layer != 8)
        {
            Destroy(parentObj);
        }
    }
    private void Update()
    {
        if(timeSpent >= maxTime)
            Destroy(parentObj);
        timeSpent += Time.deltaTime;
    }
}
