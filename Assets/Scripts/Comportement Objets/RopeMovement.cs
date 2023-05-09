// Fohndy Nomerth Tah et Jeremy Legault

using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class RopeMovement : MonoBehaviour
{
    private float angleMaximum;
    private float angle;
    private float temps;
    private float vitesse = 2f;
    private float x, y, z;

    private GameObject joueur;
    private Rigidbody rbJoueur;
    private Rigidbody rbCorde;
    RelocatePlayerAfterDownfall rpad;

    private Vector3 direction;
    private Vector3 positionJoueur;

    private bool estSurCorde;
    private bool cordeEstDisponible = true;
    private bool cordeEstEnRepos;

    private void Start()
    {
        joueur = GameObject.Find("Player");

        rbJoueur = joueur.GetComponent<Rigidbody>();
        rbCorde = GetComponent<Rigidbody>();

        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        rpad = GameObject.Find("WorldBounds").GetComponent<RelocatePlayerAfterDownfall>();
    }

    private void FixedUpdate()
    {
        // Empêche un bug de Unity
        transform.parent.position = new Vector3(x, y, z);

        if (estSurCorde)
        {
            temps += Time.deltaTime;
            angle = angleMaximum * Mathf.Sin(temps * vitesse);
            transform.parent.localRotation = Quaternion.Euler(-angle, 0, 0);

            joueur.transform.localPosition = positionJoueur;
        }

        if (cordeEstEnRepos)
        {
            temps = 0f;
            cordeEstEnRepos = false;
            transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void OnJump(InputValue value)
    {
        if (estSurCorde)
        {
            PartirDeCorde();
            cordeEstEnRepos = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && cordeEstDisponible)
        {
            rpad.swinging = true;
            angleMaximum = rbJoueur.velocity.z * 5f;
            joueur.transform.SetParent(transform);
            positionJoueur = joueur.transform.localPosition;
            estSurCorde = true;
            rbJoueur.useGravity = false;
            rbJoueur.isKinematic = true;
            cordeEstDisponible = false;
        }
    }
    IEnumerator CircumventWorldsBoundsDeath()
    {
        yield return null;
        yield return null;  // Juste pour etre safe
        rpad.swinging = false;
        StopCoroutine(CircumventWorldsBoundsDeath());
    }
    private void PartirDeCorde()
    {
        estSurCorde = false;

        rbJoueur.useGravity = true;
        rbJoueur.isKinematic = false;

        // Évite que le personnage saute dans la corde et qu'il reste donc coincé dessus.
        Invoke(nameof(MettreDisponibilitéCordeTrue), 1f);

        rbJoueur.AddForce(new Vector3(0f, Mathf.Abs(angle) * 10f * vitesse / 2f, angle * 100f * vitesse / 2f), ForceMode.Impulse);

        joueur.transform.SetParent(null);

        StartCoroutine(CircumventWorldsBoundsDeath());
    }

    private void MettreDisponibilitéCordeTrue()
    {
        cordeEstDisponible = true;
    }
}
