using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementCirculaire : MonoBehaviour
{
    [SerializeField] private float rayon = 0.65f;
    
    private float temps;
    private void Update()
    {
        temps += Time.deltaTime;

        transform.position += new Vector3(Mathf.Cos(temps) * rayon,Mathf.Sin(temps) * rayon,0);
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
