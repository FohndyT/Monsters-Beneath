using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FouetBehavior : MonoBehaviour
{
    private void Awake()
    {
        Vector3 originalScale = transform.localScale;
    }

    [SerializeField] private float DuréeDeVie = 2f;
    private float tempsÉcoulé = 0;
    private void Update()
    {
        if (DuréeDeVie < tempsÉcoulé)
            Destroy(gameObject);
        tempsÉcoulé += Time.deltaTime;
        transform.localScale -= (Vector3.forward * Time.deltaTime);
    }
}