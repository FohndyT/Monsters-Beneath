using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebondissement : MonoBehaviour
{
    private void OnCollisionEnter(Collision character)
    {
        Debug.Log("Collision detected");
        character.rigidbody.AddForce(new Vector3(0,4444,0));
    }
}
