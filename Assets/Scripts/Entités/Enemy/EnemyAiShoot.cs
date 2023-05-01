using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAiShoot : MonoBehaviour
{
    [SerializeField] private Transform cible;
    [SerializeField] private Transform shootT;
    public bool regardeJoueur = false;
    public bool peutRegarderJoueur = true;
    private float shootCooldown = 0.75f;
    public RaycastHit rayHit;
    [SerializeField] private GameObject bullet;
    public float rayHitMax { get; private set; }

    private void Awake()
    {
        rayHitMax = 100f;
    }

    void Update()
    {
        if (regardeJoueur && peutRegarderJoueur)
        {
            transform.LookAt(cible);
            if (shootCooldown >= 0.75f)
            {
                BAM();
                shootCooldown = 0f;
            }
        }

        if (shootCooldown != 0.75f)
            shootCooldown += Time.deltaTime;
    }

    private void BAM()
    {
        Vector3 shootPos = shootT.position;
        shootT.position = new Vector3(shootPos.x * Random.Range(0.95f, 1.05f), shootPos.y * Random.Range(0.95f, 1.05f),
            shootPos.z);
        Instantiate(bullet, shootT);
        shootT.DetachChildren();
    }

    private void OnTriggerStay(Collider other)
    {
        regardeJoueur = true;
        if(Physics.Raycast(transform.position, transform.forward, out rayHit, rayHitMax))
            peutRegarderJoueur = rayHit.collider.tag == "Player";
    }
    private void OnTriggerExit(Collider other)
    {
        regardeJoueur = false;
    }
}
