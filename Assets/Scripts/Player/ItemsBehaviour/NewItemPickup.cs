using UnityEngine;

public class NewItemPickup : MonoBehaviour
{
    [SerializeField] int itemIndex;
    GameObject playerObj;

    private void Awake()
    { playerObj = GameObject.Find("Player"); }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(playerObj))
        {
            playerObj.GetComponent<Player>().AcquiredItem(itemIndex);
            Destroy(gameObject);
        }
    }
}
