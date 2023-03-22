using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalLightBehavior : MonoBehaviour
{
    private bool isUsingLight;
    void Update()
    {
        isUsingLight = SpecialAttackInput.isUsingLight;
        if(!isUsingLight)
            Destroy(gameObject);
    }
    
}
