using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementCirculaire : MonoBehaviour
{
    private float temps;
    private void Update()
    {
        temps += Time.deltaTime;

        transform.position += new Vector3(Mathf.Cos(temps) * 0.65f,Mathf.Sin(temps) * 0.65f,0);
    }
    
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
