using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractInput : MonoBehaviour
{
    public bool CanInteract = false;
    
    public bool IsUsing = false;
    
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
            IsUsing = false;
        }
    }

    private void OnInteract()
    {
        IsUsing = CanInteract;
    }
}
