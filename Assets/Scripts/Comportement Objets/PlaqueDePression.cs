// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueDePression : MonoBehaviour
{
    [SerializeField] private GameObject temoin;

    private GameObject porte;
    
    private Color couleurBasePlaque;
    private Color couleurBaseTemoin;
    
    private Renderer rendererPlaque;
    private Renderer rendererTemoin;

    private void Start()
    {
        rendererPlaque = GetComponent<Renderer>();
        rendererTemoin = temoin.GetComponent<Renderer>();
        
        couleurBasePlaque = rendererPlaque.material.color;
        couleurBaseTemoin = rendererTemoin.material.color;

        porte = (temoin.transform.parent.gameObject).transform.parent.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rendererPlaque.material.color = Color.blue;
        rendererTemoin.material.color = Color.green;

        porte.GetComponent<MecanismePorte>().nombreTemoinsActive++;
    }

    private void OnCollisionExit(Collision collision)
    {
        rendererPlaque.material.color = couleurBasePlaque;
        rendererTemoin.material.color = couleurBaseTemoin;
        
        porte.GetComponent<MecanismePorte>().nombreTemoinsActive--;
    }
}
