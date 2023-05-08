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
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(playerObj))
        {
            if (gameObject.name == "Planeur")
                planage.collectedGlider = true;
            else
                playerObj.GetComponent<Player>().AcquiredItem(itemIndex);
            Destroy(gameObject);
        }
    }
}
