using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float Health = 200f; //marche en chunk de 50 donc 4 "coeur" en gros
    public GameOverScreen GameOverScreen;
    public float Iframes = 1.5f;
    public static bool canBeHit = true;
    void Update()
    {
        if (Health <= 0f )
        {
            Time.timeScale = 0;
 
            Destroy(gameObject);
            GameOverScreen.Setup();
        }
        else
            Time.timeScale = 1;


    }

    private IEnumerator OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && canBeHit)
        {
            Health -= 50f;
            transform.parent.GetComponent<Rigidbody>().AddForce(0,16000f,0);
            for (float alpha = Iframes; alpha >= 0; alpha -= 0.1f)
            {
                canBeHit = false;
                yield return null;
            }
            canBeHit = true;
        }
    }
}
