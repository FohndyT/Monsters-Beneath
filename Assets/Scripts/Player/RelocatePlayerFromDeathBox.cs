using UnityEngine;

public class RelocatePlayerFromDeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
            StartCoroutine(GameObject.Find("WorldBounds").GetComponent<RelocatePlayerAfterDownfall>().RelocatePlayer());
    }
}
