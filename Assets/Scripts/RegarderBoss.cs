// Fohndy Nomerth Tah

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegarderBoss : MonoBehaviour
{
    private GameObject boss;
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
    }
    void Update()
    {
        transform.LookAt(boss.transform);
        
    }
}
