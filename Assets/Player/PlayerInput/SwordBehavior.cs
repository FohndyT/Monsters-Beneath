using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    [SerializeField] private float DuréeDeVie = 1f;
    
    private float tempsÉcoulé = 0;
    private void Update()
    {
        if (DuréeDeVie < tempsÉcoulé)
            Destroy(gameObject);
        tempsÉcoulé += Time.deltaTime;
    }
}
