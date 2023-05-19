//Jeremy Legault

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
            case "Enemy":
                other.GetComponent<Enemy>().Hurt(4f);
                break;
            case "Grapple point":
                retainSize = true;
                break;
        }
    }
}
