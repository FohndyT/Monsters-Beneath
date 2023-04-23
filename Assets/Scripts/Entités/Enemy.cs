using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entities
{
    Player player;
    float iFramesChrono = 1f;
    static bool invincible = false;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private IEnumerator OnTriggerStay(Collider other)
    {
        if (!invincible && other.CompareTag("Player"))
        {
            player.Hurt(2f);
            invincible = true;
            while (iFramesChrono > 0)
            {
                iFramesChrono -= Time.deltaTime;
                yield return null;
            }
            iFramesChrono = 2f;
            invincible = false;
        }
    }
}
