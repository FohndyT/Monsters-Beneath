using System;
using System.Collections;
using UnityEngine;

public class Player : Entities
{
    [SerializeField] GameOverScreen GameOverScreen;
    
    Rigidbody playBody;
    float lowHealthThreshold = 1f;
    const float DboostVelo = 10f;
    float iFramesWindow = 4f;
    public int[] itemsAcquired;
    private float maxHp;
    private float healthRegen = 0f;
    public bool ouch;

    private void Awake()
    {
        maxHp = health;
        playBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(health < maxHp && !ouch)
            healthRegen += Time.deltaTime;
            if (healthRegen >= 10f)
            {
                health = maxHp;
                healthRegen = 0f;
            }
    }

    private void HealthRegen()
    {
        
    }
    void RecalculateLowHPThreshold() => lowHealthThreshold = maxHealth * 0.1f;
    void RecalculateMaxHP(float difference)
    {
        maxHealth += difference;
        RecalculateLowHPThreshold();
    }
    private IEnumerator OnTriggerStay(Collider other)
    {
        if (!invincible && other.CompareTag("Enemy") && !other.CompareTag("Sight"))
        {
            Hurt(2f);                                                         // Pour le faire jumper un peu du sol
            playBody.AddForce(0, 8000f, 0); // temp fix car velo ne fonctionne pas tjrs
            //playBody.velocity = DboostVelo * (-transform.rotation.eulerAngles + new Vector3(0f, .5f, 0f));
            ouch = true;
            StartCoroutine(IFrames(iFramesWindow));
            for (float alpha = 8f; alpha >= 0; alpha -= 0.1f)
                yield return null;
            ouch = false;
        }
    }
    private void OnCollisionEnter(Collision collision) //Est nécessaire pour la collision des attaques à distance
    {
        if (!invincible && collision.collider.CompareTag("EnemyProjectile"))
        {
            Hurt(2f);                                                        
            playBody.AddForce(0, 16000f, 0);
            StartCoroutine(IFrames(iFramesWindow));
        }
    }
    public override void Hurt(float damage)
    {
        health -= damage;
        switch (health)
        {
            case <= 0:
                StartCoroutine(Die());
                GameOverScreen.Setup();
                break;
            case var value when value < lowHealthThreshold:
                //Sound effect, ou bien change texture shader for blinking one
                break;

        }
    }
    protected override IEnumerator Die()
    {
        GameOverScreen.Setup();
        yield return StartCoroutine(base.Die());
        StopCoroutine(Die());
    }
    void ItemAlreadyAcquired()
    {
        // " You defied the Matrix. They are coming for you. "
        Debug.LogWarning("Watch out! : Item duplication");
    }
    public void AcquiredItem(int itemIndex)
    {
        int nbItems = itemsAcquired.Length;
        int itemIndexAcquiredArray = 0;
        int[] newArray = new int[nbItems + 1];

        for (int i = 0; i < nbItems; i++)
        {
            if (itemIndex > itemsAcquired[i])
                itemIndexAcquiredArray = i;
            else if (itemIndex == itemsAcquired[i])
            {
                ItemAlreadyAcquired();
                return;
            }
            else { break; }
        }

        if (itemIndexAcquiredArray == nbItems)
        {
            for (int j = 0; j < nbItems; j++)
                newArray[j] = itemsAcquired[j];
            newArray[nbItems] = itemIndex;

            itemsAcquired = newArray; // Dupliqué dans chaque blocs, car si erreur d'index se produit, au moins l'inventaire ne se fera pas wipe out.
        }
        else
        {
            for (int k = 0; k <= itemIndexAcquiredArray; k++)
                newArray[k] = itemsAcquired[k];
            newArray[itemIndexAcquiredArray + 1] = itemIndex;
            for (int l = itemIndexAcquiredArray + 2; l <= nbItems; l++)
                newArray[l] = itemsAcquired[l - 1];

            itemsAcquired = newArray;
        }
    }
}
