using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entities
{
    Player player;
    Rigidbody body;
    const float iFramesWindow = 1f;
    public bool isFrozen = false;
    public bool backingAway = false;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        body = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!player.invincible && other.CompareTag("Player")) { player.Hurt(2f); }

        //if (other.CompareTag("PlayerAttack") && )
    }
    
    public override void Hurt(float damage)
    {
        base.Hurt(damage);
        StartCoroutine(IFrames(iFramesWindow));
    }

    IEnumerator BackAway()
    {
        backingAway = true;
        float timeLimit = 5f;
        while (backingAway && timeLimit > 0f)
        {                                                                                    // (2 * PI, pas besoin d'etre exact)
            body.velocity = -Vector3.RotateTowards(body.velocity, player.transform.position, 6.2832f, 0f).normalized;
            transform.LookAt(player.transform.position);
            timeLimit -= Time.deltaTime;
            yield return null;
        }
        backingAway = false;
        StopCoroutine(BackAway());
    }
    void Freeze() => isFrozen = !isFrozen;      // Ger� par EnemyAI ?
}
