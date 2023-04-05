using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationBossFinal : MonoBehaviour
{
    private GameObject boss;
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.GetComponent<BossComportement>().phase = 1;
            Destroy(gameObject);
        }
    }
}
