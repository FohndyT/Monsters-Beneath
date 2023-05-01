// Fohndy Nomerth Tah

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private bool axeX, axeY, axeZ;
    [SerializeField] private float vitesse = 0.2f;
    void Update()
    {
        if (axeX)
        {
            transform.Rotate(new Vector3(transform.position.x,0,0),vitesse);

        }
        if (axeY)
        {
            transform.Rotate(new Vector3(0,transform.position.y,0),vitesse);
        }
        if (axeZ)
        {
            transform.Rotate(new Vector3(0,0,transform.position.z),vitesse);

        }
    }
}
