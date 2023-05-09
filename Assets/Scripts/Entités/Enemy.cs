using System.Collections;
using UnityEngine;

public class Enemy : Entities
{
    Player player;
    InputsManager inputsMana;
    Rigidbody body;
    const float iFramesWindow = 1f;
    public bool isFrozen = false;
    public bool backingAway = false;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        inputsMana = player.GetComponent<InputsManager>();
        body = GetComponent<Rigidbody>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerAttack")) { Hurt(2f); }
    }
    public override void Hurt(float damage)
    {
        if (!invincible)
        {
            base.Hurt(damage);
            StartCoroutine(IFrames(iFramesWindow));
        }
    }
    protected override IEnumerator Die()
    {
        inputsMana.zTargeting = false;
        yield return StartCoroutine(base.Die());
        StopCoroutine(Die());
    }
    IEnumerator BackAway()
    {
        backingAway = true;
        float timeLimit = 5f;
        while (backingAway && timeLimit > 0f)
        {                                                                                    // (2 * PI, pas besoin d'etre exact)
            body.velocity = -Vector3.RotateTowards(body.velocity, player.transform.position, 6.3f, 0f).normalized;
            transform.LookAt(player.transform.position);
            timeLimit -= Time.deltaTime;
            yield return null;
        }
        backingAway = false;
        StopCoroutine(BackAway());
    }
    void Freeze() => isFrozen = !isFrozen;      // Gere par EnemyAI ?
}