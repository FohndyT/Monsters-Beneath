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
            if (shootCooldown >= 1.5f)
            {
                BAM();
                shootCooldown = 0f;
            }
        }

        if (shootCooldown != 1.5f)
            shootCooldown += Time.deltaTime;
    }

    private void BAM()
    {
        Vector3 originalShootPos = shootT.position;
        Vector3 shootPos = originalShootPos;
        shootT.position = new Vector3(shootPos.x * Random.Range(0.85f,1.15f), shootPos.y * Random.Range(0.90f,1.1f),
            shootPos.z);
        Instantiate(bullet, shootT);
        shootT.position = originalShootPos;
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
