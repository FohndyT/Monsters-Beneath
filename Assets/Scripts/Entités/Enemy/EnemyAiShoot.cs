using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAiShoot : MonoBehaviour
{
    [SerializeField] private Transform cible;
    [SerializeField] private Transform shootT;
    Enemy enemyScript;
    NavMeshAgent agent;
    private Collider hurtBox;
    public bool regardeJoueur = false;
    public bool peutRegarderJoueur = true;
    private float shootCooldown = 0.75f;
    public RaycastHit rayHit;
    [SerializeField] private GameObject bullet;
    public float rayHitMax { get; private set; }

    private void Awake()
    {
        rayHitMax = 100f;
        agent = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<Enemy>();
        hurtBox = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, rayHitMax) &&  rayHit.collider.tag == "Player")
        {
            transform.LookAt(cible);
            if (shootCooldown >= 1.5f)
            {
                BAM();
                shootCooldown = Random.Range(0f,0.75f);
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
        if (Random.Range(0, 100) <= 20)
            agent.SetDestination(transform.position + Vector3.back);
        if (Random.Range(0, 100) <= 20)
            agent.SetDestination(transform.position + Vector3.right);
        if (Random.Range(0, 100) <= 20)
            agent.SetDestination(transform.position + Vector3.left);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack") && name == hurtBox.name)
        {
            enemyScript.Hurt(2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        /*regardeJoueur = true;
        if(Physics.Raycast(transform.position, transform.forward, out rayHit, rayHitMax)) //Conflit de trigger/ manque de temps pour vraiment voir comment fix
            peutRegarderJoueur = rayHit.collider.tag == "Player";*/
        transform.LookAt(cible);
    }
    private void OnTriggerExit(Collider other)
    {
        regardeJoueur = false;
    }
}
