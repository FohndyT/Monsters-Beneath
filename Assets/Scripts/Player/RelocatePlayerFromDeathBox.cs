using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocatePlayerFromDeathBox : MonoBehaviour
{
    Transform transPlayer;
    BoxCollider deathBox;
    RelocatePlayerAfterDownfall rpad;
    void Awake()
    {
        transPlayer = GameObject.Find("Player").transform;
        deathBox = GetComponent<BoxCollider>();
        rpad = GameObject.Find("WorldBounds").GetComponent<RelocatePlayerAfterDownfall>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
            StartCoroutine(rpad.RelocatePlayer());
    }
}
