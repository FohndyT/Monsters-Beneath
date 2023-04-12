// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TranslationVerticale : MonoBehaviour
{
    [SerializeField] private float grandeurMouvement = 1;

    [SerializeField] private Transform pointsApparition0;
    [SerializeField] private Transform pointsApparition1;
    [SerializeField] private Transform pointsApparition2;
    [SerializeField] private Transform pointsApparition3;
    [SerializeField] private Transform pointsApparition4;

    private Transform[] pointsApparition = new Transform[5];
    private float position;
    private float temps;
    private int nombre;

    private void Start()
    {
        // StartCoroutine(Attente());
        
        pointsApparition[0] = pointsApparition0;
        pointsApparition[1] = pointsApparition1;
        pointsApparition[2] = pointsApparition2;
        pointsApparition[3] = pointsApparition3;
        pointsApparition[4] = pointsApparition4;
        
        transform.position = pointsApparition[0].position;
    }

    void FixedUpdate()
    {
        temps += Time.deltaTime;
        position = Mathf.Sin(temps);
        transform.position += new Vector3(0, grandeurMouvement * position / 25f, 0);
    }

    private void ChoisirApparition()
    {
        if (gameObject.tag == "Slime")
        {
            nombre = Random.Range(0, 5);
            transform.position = pointsApparition[nombre].position;
        }
    }

    /* IEnumerator Attente()
    {
        while (true)
        {
            if (gameObject.tag == "Slime")
            {
                nombre = Random.Range(0, 5);
                transform.position = pointsApparition[nombre].position;
                
                yield return new WaitForSeconds(4f);
            }
        }
    } */
}
