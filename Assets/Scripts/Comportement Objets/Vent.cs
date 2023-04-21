// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    private Vector3 forceVent;
    private Planage planage;
    private Rigidbody rb;

    private bool estEnVol;

    private void Start()
    {
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        planage = rb.gameObject.GetComponent<Planage>();
    }

    /* private void Update()
    {
        if (estEnVol)
        {
            rb.velocity = forceVent;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (planage.estCree)
            {
                Debug.Log("Collision Vent");
            
                // estEnVol = true;
                planage.vitessePara = 8f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // estEnVol = false;
            planage.vitessePara = -2f;
        }
    }
}
