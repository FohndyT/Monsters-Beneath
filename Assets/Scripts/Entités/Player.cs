using System.Collections;
using UnityEngine;

public class Player : Entities
{
    //public GameOverScreen GameOverScreen;
    float lowHealthThreshold = 1f;
    float iFramesChrono = 2f;
    static bool invincible = false;
    public int[] itemsAcquired = new int[0];

    void RecalculateLowHPThreshold() => lowHealthThreshold = maxHealth * 0.1f;
    void RecalculateMaxHP(float difference)
    {
        maxHealth += difference;
        RecalculateLowHPThreshold();
    }
    private IEnumerator OnTriggerStay(Collider other)
    {
        if (!invincible && other.CompareTag("Enemy"))
        {
            Hurt(2f);                                                                                     // Pour le faire jumper un peu du sol
            transform.parent.GetComponent<Rigidbody>().AddForce(16000f * (-transform.rotation.eulerAngles + new Vector3(0.5f, 0.5f, 0.5f)));
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
    public override void Hurt(float damage)
    {
        health -= damage;
        switch (health)
        {
            case <= 0:
                StartCoroutine(Die());
                break;
            case var value when value < lowHealthThreshold:
                //Sound effect, ou bien change texture for blinking one
                break;

        }
    }
    protected override IEnumerator Die()
    {
        Time.timeScale = 0;
        yield return StartCoroutine(base.Die());
        //GameOverScreen.Setup();
        StopCoroutine(Die());
    }
    void ItemAlreadyAcquired()
    {
        // " They are coming for you. You should have stayed put, yet you defied the Matrix. They are coming for you. "
        Debug.LogWarning("Item duplication");
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
