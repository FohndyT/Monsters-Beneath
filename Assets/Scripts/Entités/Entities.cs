using System.Collections;
using UnityEngine;

public abstract class Entities : MonoBehaviour
{
    [SerializeField] protected float health = 10f;
    protected float maxHealth = 10f;    // 1 pt = un demi coeur. Donc de base, max health = 5 coeurs
    float iFramesChrono;
    public bool invincible = false;

    public virtual void Hurt(float damage)
    {
        health -= damage;

        if (health <= 0)
            StartCoroutine(Die());
    }

    protected virtual IEnumerator Die()
    {
        //Death animation
        Destroy(gameObject);
        yield return null;
        StopCoroutine(Die());
    }

    protected IEnumerator IFrames(float invincibilityWindow)
    {
        invincible = true;
        for (iFramesChrono = 0f; iFramesChrono < invincibilityWindow; iFramesChrono += Time.deltaTime)
            yield return null;
        invincible = false;
        StopCoroutine(IFrames(0f));
    }
}
