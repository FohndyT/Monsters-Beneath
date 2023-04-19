// Fohndy Nomerth Tah

using UnityEngine;

public class RegarderJoueur : MonoBehaviour
{
    [SerializeField] private Transform cible;
    public bool regardeJoueur = false;
    void Update()
    {
        transform.LookAt(cible);
    }
}
