// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortVélocité : MonoBehaviour
{
    private Player joueurPlayer;

    [SerializeField] private float vitesseMort = 20f;

    private void Start()
    {
        joueurPlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Rigidbody>().velocity.y > vitesseMort)
        {
            joueurPlayer.Hurt(10);
        }
    }
}
