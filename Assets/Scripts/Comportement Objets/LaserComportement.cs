using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserComportement : MonoBehaviour
{
    private LineRenderer rayon;
    private RaycastHit pointDeFrappe;
    
    void Start()
    {
        rayon = GetComponent<LineRenderer>();
    }

    void Update()
    {
        rayon.SetPosition(0,transform.position);
        if (Physics.Raycast(transform.position,transform.forward, out pointDeFrappe))
        {
            if (pointDeFrappe.collider)
            {
                rayon.SetPosition(1,pointDeFrappe.point);
            }
        }
        else
        {
            rayon.SetPosition(1,transform.position + transform.forward * 1000);
        }
        
        // Juste pour les tests, à enlever si implimenté dans le jeu
        if (Input.GetKey("a"))
        {
            transform.Rotate(-Vector3.up / 2,Space.Self);
        }
        if (Input.GetKey("d"))
        {
            transform.Rotate(Vector3.up / 2,Space.Self);
        }
    }
}
