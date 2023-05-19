//Jeremy Legault

using System.Collections;
using UnityEngine;

public class NewItemPickup : MonoBehaviour
{
    [SerializeField] int itemIndex;
    GameObject playerObj;
    Planage planage;
    private void Awake()
    {
        playerObj = GameObject.Find("Player");
        planage = playerObj.GetComponent<Planage>();
        gameObject.SetActive(false);
    }
    private void Start()
    { StartCoroutine(CircumventGettingAllItemsAtRestart()); }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(playerObj))
        {
            if (gameObject.name == "Planeur")
                planage.collectedGlider = true;
            else if (gameObject.name == "Sword")
                playerObj.GetComponent<InputsManager>().acquiredSword = true;
            else
                playerObj.GetComponent<Player>().AcquiredItem(itemIndex);
            Destroy(gameObject);
        }
    }
    IEnumerator CircumventGettingAllItemsAtRestart()
    {
        yield return new WaitForSeconds(2f);
        yield return null;  // Just to be safe
        gameObject.SetActive(true);
        StopCoroutine(CircumventGettingAllItemsAtRestart());
    }
}
