// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Planage : MonoBehaviour
{
    [SerializeField] private GameObject planeur;
    [NonSerialized] public float vitesseParachute;
    private GameObject clonePlaneur;
    private Rigidbody rb;

    // À modifier plus tard
    private bool planeurEstCollecté;
    
    [NonSerialized] public bool estCréé;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        vitesseParachute = -2f;
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

        MettreVitesse();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planeur")
        {
            planeurEstCollecté = true;
        }
    }

    private void MettreVitesse()
    {
        if (estCréé)
        {
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, vitesseParachute, rb.velocity.z);
        }
        else
        {
            rb.useGravity = true;
        }
    }
}
