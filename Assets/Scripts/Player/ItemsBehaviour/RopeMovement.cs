// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class RopeMovement : MonoBehaviour
{
    private float angleMaximum;
    private float angle;
    private float temps;
    private float vitesse = 2f;
    private float x, y, z;
    
    private GameObject joueur;
    private Rigidbody rbCorde;
    private Rigidbody rbJoueur;

    private Vector3 direction;
    private Vector3 positionJoueur;

    private bool estSurCorde;
    private bool cordeEstDisponible = true;
    private bool cordeEstEnRepos;

    private void Start()
    {
        joueur = GameObject.Find("Player");

        rbCorde = GetComponent<Rigidbody>();
        rbJoueur = joueur.GetComponent<Rigidbody>();
        
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    private void FixedUpdate()
    {
        // Empêche un bug de Unity
        transform.parent.position = new Vector3(x, y, z);
        
        if (estSurCorde)
        {
            temps += Time.deltaTime;
            //AngleCourrant = Mathf.Sin( temps * vitesse);
            angle = angleMaximum * Mathf.Sin( temps * vitesse);
            transform.parent.localRotation = Quaternion.Euler( -angle, 0, 0);

            // while (angleMaximum >= 0f)
            // {
            //     angleMaximum -= 5f;
            // }
            //
            // if (angleMaximum < 0f)
            // {
            //     angleMaximum = 0f;
            // }

            joueur.transform.localPosition = positionJoueur;
            
            if (Input.GetKeyDown("d"))
            {
                // if (AngleCourrant < 0)
                // {
                //     angleMaximum += 10f;
                // }
                //
                // angleMaximum += 10f;
            }

            if (Input.GetKeyDown("a"))
            {
                // if (AngleCourrant > 0)
                // {
                //     angleMaximum += 10f;
                // }
                //
                // angleMaximum += 10f;
            }

            if (Input.GetKeyDown("space"))
            {
                PartirDeCorde();
                cordeEstEnRepos = true;
            }
        }

        if (cordeEstEnRepos)
        {
            temps = 0f;
            cordeEstEnRepos = false;
            transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && cordeEstDisponible)
        {
            angleMaximum = rbJoueur.velocity.z * 5f;
            joueur.transform.SetParent(transform);
            positionJoueur = joueur.transform.localPosition;
            estSurCorde = true;
            rbJoueur.useGravity = false;
            rbJoueur.isKinematic = true;
            cordeEstDisponible = false;
        }
    }

    private void PartirDeCorde()
    {
        estSurCorde = false;
        joueur.transform.SetParent(null);
        rbJoueur.useGravity = true;
        rbJoueur.isKinematic = false;
        Invoke("MettreDisponibilitéCordeTrue",1f);

        // rbJoueur.velocity = new Vector3(0f, /*Mathf.Abs(angle) * */rbCorde.velocity.y * 500f, rbCorde.velocity.z * 500f/*angle * 3f*/);

        rbJoueur.AddForce(new Vector3(0f,Mathf.Abs(angle) * 20f,angle * 100f),ForceMode.Impulse);
    }

    private void MettreDisponibilitéCordeTrue()
    {
        cordeEstDisponible = true;
    }

    private void DiminutionAngleMaximum()
    {
        if (estSurCorde == false)
        {
            if (angleMaximum > 0)
            {
                Invoke("DiminuerAngleMax",2f);
            }
            
            if (angleMaximum < 0)
            {
                angleMaximum = 0;
            }
        }
    }

    private void DiminuerAngleMax()
    {
        angleMaximum -= 5f;
    }
}
