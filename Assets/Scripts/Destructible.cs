using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private Player joueurPlayer;

    private void Start()
    {
        joueurPlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Soin"))
            {
                joueurPlayer.Hurt(-10f);
            }
        }
    }
}
