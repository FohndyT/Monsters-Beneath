// Fohndy Nomerth Tah

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// https://www.youtube.com/watch?v=kzHNUT9q4JE&ab_channel=BaDuy

[RequireComponent(typeof(LineRenderer))]
public class LaserComportement : MonoBehaviour
{
    // [SerializeField] private bool up, down, left, right, forward, backward;
    // private Vector3 directionRayon;
    
    [SerializeField] private float longueurRayon = 500;

    private LineRenderer lr;
    private RaycastHit frappe;
    private Ray rayon;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        rayon = new Ray(transform.position,transform.forward);

        // Nombre de points (verticies)
        lr.positionCount = 1;
        // Met la position du premier point à la position du gameObject
        lr.SetPosition(0,transform.position);
        float longueurÀTraverser = longueurRayon;

        for (int i = 0; i < 10; i++)
        {
            if (Physics.Raycast(rayon.origin, rayon.direction, out frappe, longueurÀTraverser))
            {
                lr.positionCount += 1;
                lr.SetPosition(lr.positionCount - 1, frappe.point);
                longueurÀTraverser -= Vector3.Distance(rayon.origin, frappe.point);
                
                rayon = new Ray(frappe.point, Vector3.Reflect(rayon.direction, frappe.normal));

                if (frappe.collider.tag != "Mirroir")
                {
                    if (frappe.collider.CompareTag("Bois") && !transform.parent.CompareTag("Mirroir"))
                    {
                        //Destroy(frappe.transform.gameObject);
                    }

                    break;
                }
            }
            else
            {
                lr.positionCount += 1;
                lr.SetPosition(lr.positionCount - 1, rayon.origin + rayon.direction * longueurÀTraverser);
            }
        }

        /* void Direction()
        {
            if (up)
            {
                directionRayon = transform.up;
            }
            if (down)
            {
                directionRayon = -transform.up;
            }
            if (left)
            {
                directionRayon = -transform.right;
            }
            if (right)
            {
                directionRayon = transform.right;
            }
            if (forward)
            {
                directionRayon = transform.forward;
            }

            if (backward)
            {
                directionRayon = -transform.forward;
            }
        }*/

        /////////////////////////////////////////////////////////////////////////////////////////
        
        /* if (Physics.Raycast(transform.position,transform.forward, out frappe))
        {
            if (frappe.collider)
            {
                lr.SetPosition(1,frappe.point);
            }
        }
        else
        {
            lr.SetPosition(1,transform.position + transform.forward * 500);
        } 
        
        // Juste pour les tests, à enlever si implimenté dans le jeu
        if (Input.GetKey("a"))
        {
            transform.Rotate(-Vector3.up / 3,Space.Self);
        }
        if (Input.GetKey("d"))
        {
            transform.Rotate(Vector3.up / 3,Space.Self);
        } */
    }
}
