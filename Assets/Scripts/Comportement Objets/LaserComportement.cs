// Fohndy Nomerth Tah

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

// https://www.youtube.com/watch?v=kzHNUT9q4JE&ab_channel=BaDuy
// https://www.google.com/search?q=how+to+refract+a+ray+unity&rlz=1C1ONGR_frCA1022CA1022&oq=how+to+refract+a+ray+unity&aqs=chrome..69i57j33i160l2.15732j0j7&sourceid=chrome&ie=UTF-8#fpstate=ive&vld=cid:0d0786ef,vid:r0Oz8FiiSUI

[RequireComponent(typeof(LineRenderer))]
public class LaserComportement : MonoBehaviour
{
    // [SerializeField] private bool up, down, left, right, forward, backward;
    // private Vector3 directionRayon;
    
    [SerializeField] private float longueurRayon = 500;
    [Range(1f,50f)] [SerializeField] private float indiceDeRefraction = 20f;

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

        //for (int i = 0; i < 10; i++)
        
        while(true)
        {
            if (Physics.Raycast(rayon.origin, rayon.direction, out frappe, longueurÀTraverser))
            {
                lr.positionCount += 1;
                lr.SetPosition(lr.positionCount - 1, frappe.point);
                longueurÀTraverser -= Vector3.Distance(rayon.origin, frappe.point);
                
                if (frappe.collider.CompareTag("Mirroir"))
                {
                    rayon = new Ray(frappe.point, Vector3.Reflect(rayon.direction, frappe.normal));
                }
                if (frappe.collider.CompareTag("Surface Réfractante"))
                {
                    float angle = CalculerAngleRefraction(Vector3.Angle(rayon.direction, frappe.normal) * Mathf.Deg2Rad);
                    //Vector3 direction = new Vector3(Mathf.Cos(angle),0,Mathf.Sin(angle));

                    frappe.point = new Vector3(Mathf.Abs(CalculerVecteurRefraction().x) / (CalculerVecteurRefraction().x + 0.0001f) * 0.001f + frappe.point.x,
                                               Mathf.Abs(CalculerVecteurRefraction().y) / (CalculerVecteurRefraction().y + 0.0001f) * 0.001f + frappe.point.y,
                                               Mathf.Abs(CalculerVecteurRefraction().z) / (CalculerVecteurRefraction().z + 0.0001f) * 0.001f + frappe.point.z);
                    
                    rayon = new Ray(frappe.point,CalculerVecteurRefraction());
                }
                if (frappe.collider.CompareTag("Bois"))
                {
                    Destroy(frappe.transform.gameObject);
                }
                if (!frappe.collider.CompareTag("Mirroir") && !frappe.collider.CompareTag("Surface Réfractante"))
                {
                    break;
                }
            }
        }

        float CalculerAngleRefraction(float angleIncidence)
        {
            return Mathf.Asin(Mathf.Sin(angleIncidence) / indiceDeRefraction) /* * Mathf.Rad2Deg*/;
        }

        Vector3 CalculerVecteurRefraction()
        {
            return (1/indiceDeRefraction * Vector3.Cross(frappe.normal, Vector3.Cross(-frappe.normal,rayon.direction)) - frappe.normal * Mathf.Sqrt(1-Vector3.Dot(Vector3.Cross(frappe.normal,rayon.direction) * (1/indiceDeRefraction * 1/indiceDeRefraction),Vector3.Cross(frappe.normal, rayon.direction)))).normalized;
        }
        
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
