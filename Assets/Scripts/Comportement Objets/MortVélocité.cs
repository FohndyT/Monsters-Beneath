// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortVélocité : MonoBehaviour
{

    [SerializeField] private float vitesseMort = 20f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Rigidbody>().velocity.y > vitesseMort)
        {
            Debug.Log("Player is dead");
        }
    }
}
