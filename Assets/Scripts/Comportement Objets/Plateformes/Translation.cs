// Fohndy Nomerth Tah

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour
{
    [Range(-1,1)] [SerializeField] int direction;
    private float mouvement;
    private float temps;
    
    void FixedUpdate()
    {
        temps += Time.deltaTime;
        mouvement = Mathf.Sin(temps);
        transform.position += new Vector3(direction * mouvement / 3f, 0, 0);  
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
