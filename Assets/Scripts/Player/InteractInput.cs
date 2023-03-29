using UnityEngine;

public class InteractInput : MonoBehaviour
{
    public bool CanInteract = false;
    public bool isUsing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            CanInteract = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract = false;
            isUsing = false;
        }
    }
    private void OnAttack()
    { isUsing = CanInteract; }
}
