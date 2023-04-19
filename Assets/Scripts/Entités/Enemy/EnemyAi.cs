using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    CurveTraveler traveler;
    [SerializeField] private GameObject target;
    private NavMeshAgent agent;
    public bool chaseMode = false;
    public bool patrolMode = true;
    private Vector3 returnPos;
    [SerializeField] private float evadeTime = 5f;
    [SerializeField] private float waitTime = 5f;
    private float waitingTime = 0f;
    public float Health = 100f;

    private void Awake()
    {
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        traveler = GetComponent<CurveTraveler>();
        returnPos = transform.position;

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
            Health -= 50f;
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
        if (Health <= 0)
        {
            Destroy(gameObject);
            chaseMode = false;
        }
        if (patrolMode)
        {
            // maybe do something but for now CurveTraveler fait cette job
        }
        if (chaseMode)
        {
            traveler.progress = 0f;
            traveler.enabled = false;
            agent.SetDestination(target.transform.position);
        }
        else
        {
            if (waitingTime >= waitTime)
            {
                agent.SetDestination(returnPos);
                waitingTime = 0f;
            }
            waitingTime += Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, returnPos) < 1f)
        {
            traveler.enabled = true;
            patrolMode = true;
        }
    }
}
