using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FouetInterraction : MonoBehaviour
{
    bool retainSize;
    private void Awake()
    {
        retainSize = transform.parent.gameObject.GetComponent<FouetBehavior>().retainSize;
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Ennemi":
                // Call Ennemi.Hurt()
                break;
            case "Grapple point":
                retainSize = true;

                break;
            default:
                if (other.gameObject.name != "Body")
                    GameObject.Destroy(other.gameObject);   // Just for fun. Remove after tests
                break;
        }
    }
}
