using UnityEngine;

public class InteractInput : MonoBehaviour
{
    public bool canInteract = false;
    public bool isUsing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            canInteract = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            isUsing = false;
        }
    }
    private void OnAttack()
    { isUsing = canInteract; }
}
