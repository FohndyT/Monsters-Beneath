using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiShoot : MonoBehaviour
{
    [SerializeField] private Transform cible;
    public bool regardeJoueur = false;
    public bool peutRegarderJoueur = true;
    public RaycastHit rayHit;
    public float rayHitMax { get; private set; }

    private void Awake()
    {
        rayHitMax = 100f;
    }

    void Update()
    {
        if(regardeJoueur && peutRegarderJoueur)
            transform.LookAt(cible);
    }

    private void OnTriggerStay(Collider other)
    {
        regardeJoueur = true;
        if(Physics.Raycast(transform.position, transform.forward, out rayHit, rayHitMax))
            peutRegarderJoueur = rayHit.collider.tag != "Player";
    }
    private void OnTriggerExit(Collider other)
    {
        regardeJoueur = false;
    }
}
