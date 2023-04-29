// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class RopeMovement : MonoBehaviour
{
    private float angleMaximum;
    private float angle;
    private float temps;
    private float vitesse = 1f;
    private float x, y, z;
    
    private GameObject joueur;
    private Rigidbody rbCorde;
    private Rigidbody rbJoueur;

    private Vector3 direction;
    private Vector3 positionJoueur;

    private bool estSurCorde;
    private bool cordeEstDisponible = true;

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
            angle = angleMaximum * Mathf.Sin( temps * vitesse);
            
            transform.parent.localRotation = Quaternion.Euler( -angle, 0, 0);

            joueur.transform.localPosition = positionJoueur;
            
            if (Input.GetKeyDown("d"))
            {
                
            }

            if (Input.GetKeyDown("a"))
            {
                
            }

            if (Input.GetKeyDown("space"))
            {
                PartirDeCorde();
            }
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
        
        rbJoueur.AddForce(new Vector3(0f,0f,-angle * 10f));
    }

    private void MettreDisponibilitéCordeTrue()
    {
        cordeEstDisponible = true;
    }
}
