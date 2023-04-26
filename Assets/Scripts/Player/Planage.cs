using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Planage : MonoBehaviour
{
    [SerializeField] private GameObject planeur;
    private GameObject clonePlaneur;

    // À modifier plus tard
    private bool planeurEstCollecté = false;
    public float vitessePara;
    public bool estCree;

    private void Update()
    {
        if (planeurEstCollecté)
        {
            if (Input.GetKey("1") && !estCree)
            {
                clonePlaneur = Instantiate(planeur, transform.localPosition + new Vector3(0,0.7499999f,0),transform.rotation);
                clonePlaneur.transform.parent = transform;
                
                estCree = true;
            }

            if (Input.GetKey("2") && estCree)
            {
                Destroy(clonePlaneur);
                estCree = false;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planeur")
        {
            planeurEstCollecté = true;
        }
    }
}
