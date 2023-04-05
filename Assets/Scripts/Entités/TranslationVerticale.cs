using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationVerticale : MonoBehaviour
{
    [SerializeField] private float grandeurMouvement = 1;
    private float mouvement;
    private float temps;
    
    void Update()
    {
        temps += Time.deltaTime;
        mouvement = Mathf.Sin(temps);
        transform.position += new Vector3(0, grandeurMouvement * mouvement / 25f, 0);  
    }
}
