// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TranslationVerticale : MonoBehaviour
{
    [SerializeField] private float grandeurMouvement = 1;

    private float position;
    private float temps;
    private int nombre;

    void FixedUpdate()
    {
        temps += Time.deltaTime;
        position = Mathf.Sin(temps);
        transform.position += new Vector3(0, grandeurMouvement * position / 25f, 0);
    }
}
