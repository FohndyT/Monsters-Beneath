using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegarderJoueur : MonoBehaviour
{
    [SerializeField] private Transform cible;
    void Update()
    {
        
        transform.LookAt(cible);
        
    }
}
