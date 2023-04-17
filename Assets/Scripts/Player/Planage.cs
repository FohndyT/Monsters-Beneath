// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Planage : MonoBehaviour
{
    [SerializeField] private GameObject planeur;
    [SerializeField] private float vitesseDescente = -2f;
    private GameObject clonePlaneur;
    private Rigidbody rb;

    // À modifier plus tard
    private bool planeurEstCollecté;
    
    private bool estCréé;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (planeurEstCollecté)
        {
            if (Input.GetKey("1") && !estCréé)
            {
                clonePlaneur = Instantiate(planeur, transform.localPosition + new Vector3(0,0.7499999f,0),transform.rotation);
                clonePlaneur.transform.parent = transform;
                
                estCréé = true;
            }

            if (Input.GetKey("2") && estCréé)
            {
                Destroy(clonePlaneur);
                estCréé = false;
            }
        }

        VitesseDescente();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planeur")
        {
            planeurEstCollecté = true;
        }
    }

    private void VitesseDescente()
    {
        if (estCréé)
        {
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, vitesseDescente, rb.velocity.z);
        }
        else
        {
            rb.useGravity = true;
        }
    }
}
