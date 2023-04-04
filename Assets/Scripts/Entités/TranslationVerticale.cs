using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationVerticale : MonoBehaviour
{
    [SerializeField] private float grandeurMouvement = 5;
    private float mouvement;
    private float temps;
    
    void Update()
    {
        temps += Time.deltaTime;
        mouvement = Mathf.Sin(temps);
        transform.position += new Vector3(0, grandeurMouvement * mouvement, 0);  
    }
}
