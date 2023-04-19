// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournerObjet : MonoBehaviour
{
    private bool estControléParJoueur;

    void Update()
    {
        if (estControléParJoueur)
        {
            if (Input.GetKey("k"))
            {
                transform.parent.Rotate(-Vector3.up / 3,Space.Self);
            }
            
            if (Input.GetKey("j"))
            {
                transform.parent.Rotate(Vector3.up / 3,Space.Self);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            estControléParJoueur = true;
        }
    }
    
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            estControléParJoueur = false;
        }
    }
}
