using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation2Component : MonoBehaviour
{
    private float mouvement;
    private float temps;
    
    void Update()
    {
        temps += Time.deltaTime;
        mouvement = Mathf.Sin(temps);
        transform.position += new Vector3(-mouvement / 3f, 0, 0);  
    }

    // https://www.youtube.com/watch?v=dWtjKKR7Q3s&ab_channel=CodinginFlow
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
