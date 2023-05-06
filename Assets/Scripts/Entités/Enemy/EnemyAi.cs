using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAi : MonoBehaviour
{
    CurveTraveler traveler;
    NavMeshAgent agent;
    Enemy enemyScript;
    public RaycastHit rayHit;
    [SerializeField] GameObject target;
    [SerializeField] bool chaseMode = false;
    [SerializeField] bool patrolMode = true;
    [SerializeField] bool returnFromChase = false;
    [SerializeField] float evadeTime = 5f;
    [SerializeField] float waitTime = 5f;
    float waitingTime = 0f;
    Vector3 returnPos;
    public float Health = 100f;

    public float rayHitMax { get; private set; }
    private void Awake()
    {
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        traveler = GetComponent<CurveTraveler>();
        enemyScript = GetComponent<Enemy>();
        returnPos = transform.position;
        rayHitMax = 80f;

    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chaseMode = true;
            patrolMode = false;
            traveler.enabled = false;
        }
        if (other.CompareTag("PlayerAttack"))
        {
            enemyScript.Hurt(2f);
            for (float alpha = 1.5f; alpha >= 0; alpha -= 0.1f)
            {
                agent.SetDestination(transform.position);
                yield return null;
            }
        }
    }
    
    private IEnumerator OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (float alpha = evadeTime; alpha >= 0; alpha -= 0.1f)
            {
                agent.SetDestination(target.transform.position);
                chaseMode = false;
                yield return null;
            }
        }
    }

    void Update()
    {
        if (patrolMode)
        {
            // maybe do something but for now CurveTraveler fait cette job
            if(Physics.Raycast(transform.position, transform.forward, out rayHit, rayHitMax) || Physics.Raycast(transform.position, transform.forward + Vector3.right/3, out rayHit, rayHitMax) || Physics.Raycast(transform.position, transform.forward+ Vector3.left/3, out rayHit, rayHitMax))
                chaseMode = rayHit.collider.tag == "Player";
        }
        if (chaseMode)
        {
            traveler.progress = 0f;
            traveler.enabled = false;
            if (Random.Range(0, 100) <= 30)
                agent.SetDestination(transform.position - Vector3.left);
            else
                agent.SetDestination(target.transform.position);
        }
        else
        {
            if (waitingTime >= waitTime)
            {
                agent.SetDestination(returnPos);
                returnFromChase = true;
                waitingTime = 0f;
            }
            waitingTime += Time.deltaTime;
        }

        if (returnFromChase && Vector3.Distance(transform.position, returnPos) < 1f)
        {
            traveler.enabled = true;
            patrolMode = true;
            returnFromChase = false;
        }
    }
}
