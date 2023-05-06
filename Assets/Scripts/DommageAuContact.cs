// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DommageAuContact : MonoBehaviour
{
    [SerializeField] private float dommage;

    private Player joueurPlayer;

    private void Start()
    {
        joueurPlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            joueurPlayer.Hurt(dommage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            joueurPlayer.Hurt(dommage);
        }
    }
}
