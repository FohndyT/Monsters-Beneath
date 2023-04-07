// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementPendule : MonoBehaviour
{
    [SerializeField] float angleMaximum = 50.0f;
    [SerializeField] float vitesse = 1.0f;
    private float angle;
    private float temps;

    private float x, y, z;

    private void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    private void Update()
    {
        temps += Time.deltaTime;
        angle = angleMaximum * Mathf.Sin( temps * vitesse);
        
        // Empêche un bug de Unity
        
        transform.position = new Vector3(x, y, z);
        transform.localRotation = Quaternion.Euler( 0, 0, angle);
    }
}
