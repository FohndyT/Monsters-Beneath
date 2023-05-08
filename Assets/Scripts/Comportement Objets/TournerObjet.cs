// Fohndy Nomerth Tah et Jeremy Legault

using UnityEngine;
using UnityEngine.InputSystem;

public class TournerObjet : MonoBehaviour
{
    private bool estControleParJoueur;
    bool left;
    bool right;

    void Update()
    {
        if (estControleParJoueur)
        {
            if (left)
                transform.parent.Rotate(Vector3.up / 3, Space.Self);
            if (right)
                transform.parent.Rotate(-Vector3.up / 3, Space.Self);
        }
    }
    void OnRotateLeft(InputValue value) => left = !left;
    void OnRotateRight(InputValue value) => right = !right;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            estControleParJoueur = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            estControleParJoueur = false;
        }
    }
}
