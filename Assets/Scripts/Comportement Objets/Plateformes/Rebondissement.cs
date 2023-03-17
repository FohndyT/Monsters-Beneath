using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebondissement : MonoBehaviour
{
    [SerializeField] float forceRessort = 4444;
    private void OnCollisionEnter(Collision character)
    {
        Debug.Log("Collision detected");
        character.rigidbody.AddForce(new Vector3(0,forceRessort,0));
    }
}
