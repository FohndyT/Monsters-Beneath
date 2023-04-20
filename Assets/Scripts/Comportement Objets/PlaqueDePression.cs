// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueDePression : MonoBehaviour
{
    private Color couleurBase;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        couleurBase = renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        renderer.material.color = Color.blue;
    }

    private void OnCollisionExit(Collision collision)
    {
        renderer.material.color = couleurBase;
    }
}
