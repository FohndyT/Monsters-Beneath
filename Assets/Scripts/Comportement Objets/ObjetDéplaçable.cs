// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjetDéplaçable : MonoBehaviour
{
    private Vector3 direction;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            direction = collision.gameObject.transform.position - transform.position;
            direction.Normalize();
        
            collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(direction,transform.position,ForceMode.Impulse);
        }
    }
}
