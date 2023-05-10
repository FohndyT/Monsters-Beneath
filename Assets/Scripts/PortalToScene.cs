using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene("Main Dungeon");
    }
}
