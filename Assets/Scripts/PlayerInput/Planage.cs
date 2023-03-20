using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Planage : MonoBehaviour
{
    [SerializeField] GameObject planeur;

    // À modifier plus tard
    private bool planeurEstCollecté = true;
    
    private bool estCréé;

    private void Update()
    {
        if (planeurEstCollecté)
        {
            if (Input.GetKey("1") && !estCréé)
            {
                Instantiate(planeur, planeur.transform.localPosition,Quaternion.identity);
                estCréé = true;
            }

            if (Input.GetKey("2") && estCréé)
            {
                Destroy(planeur);
                estCréé = false;
            }
        }
    }
}
