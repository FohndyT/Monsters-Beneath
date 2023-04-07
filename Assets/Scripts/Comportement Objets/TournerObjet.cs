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
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.GetChild(0).Rotate(-Vector3.up / 3,Space.Self);
            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.GetChild(0).Rotate(Vector3.up / 3,Space.Self);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            estControléParJoueur = true;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            estControléParJoueur = false;
        }
    }
}
