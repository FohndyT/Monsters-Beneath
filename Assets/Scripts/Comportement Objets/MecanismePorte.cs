// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanismePorte : MonoBehaviour
{
    private GameObject porte;
    
    private Vector3 positionFerme;
    private Vector3 positionOuvert;

    private float time;

    private bool estActive;
    [NonSerialized] public int nombreTemoinsActive;
    
    void Start()
    {
        positionFerme = transform.position;
        positionOuvert = new Vector3(transform.position.x, transform.position.y - 8, transform.position.z);
    }

    void Update()
    {
        time += Time.deltaTime;
        
        Activation();
        
        if (estActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionOuvert,0.1f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, positionFerme, 0.1f);
        }
    }

    void Activation()
    {
        if (nombreTemoinsActive == 5)
        {
            Debug.Log("Activé");
            estActive = true;

            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            estActive = false;
            
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        }
    }
}
