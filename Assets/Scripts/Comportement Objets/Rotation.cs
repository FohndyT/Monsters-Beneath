// Fohndy Nomerth Tah

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float vitesse = 0.2f;
    private float temps;
    void Update()
    {
        transform.Rotate(new Vector3(0,transform.position.y,0),vitesse);
    }
}
