using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float Health = 200f;
    private bool isAlive = true;
    public GameOverScreen GameOverScreen;
    public float Iframes = 1.5f;
    public static bool canBeHit = true;

    

    void Update()
    {
        if (Health <= 0f && isAlive)
        {
            isAlive = false;
            Destroy(gameObject);
            GameOverScreen.Setup();
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && canBeHit)
        {
            Health -= 50f;
            
            for (float alpha = Iframes; alpha >= 0; alpha -= 0.1f)
            {
                canBeHit = false;
                yield return null;
            }

            canBeHit = true;
        }
    }
}
