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

    private void Awake()
    {
        playBody = GetComponent<Rigidbody>();
    }
    void RecalculateLowHPThreshold() => lowHealthThreshold = maxHealth * 0.1f;
    void RecalculateMaxHP(float difference)
    {
        maxHealth += difference;
        RecalculateLowHPThreshold();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!invincible && other.CompareTag("Enemy"))
        {
            Hurt(2f);                                                         // Pour le faire jumper un peu du sol
            playBody.AddForce(0, 8000f, 0); // temp fix car velo ne fonctionne pas tjrs
            //playBody.velocity = DboostVelo * (-transform.rotation.eulerAngles + new Vector3(0f, .5f, 0f));
            StartCoroutine(IFrames(iFramesWindow));
        }
    }
<<<<<<< HEAD
    private void OnCollisionEnter(Collision collision) //Est nécessaire pour la collision des attaques à distance
    {
        if (!invincible && collision.collider.CompareTag("EnemyProjectile"))
        {
            Hurt(2f);                                                        
            playBody.AddForce(0, 16000f, 0);
            StartCoroutine(IFrames(iFramesWindow));
        }
    }
=======
>>>>>>> d94e8364dc2660dad4eea9f8d9b8fe4d4db942cf
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
