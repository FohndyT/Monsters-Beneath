using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private NavMeshAgent agent;
    private Transform obj;
    [SerializeField] private Transform[] waypoints;
    public bool chaseMode;
    public bool patrolMode = true;
    private int currentWaypoint = 0;
    private Vector3 originalPosition;
    [SerializeField] private float evadeTime = 5f;
    [SerializeField] private float waitTime = 5f;
    private float waitingTime = 0f;

    private void Awake()
    {
        obj = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chaseMode = true;
            patrolMode = false;
        }
    }

    private IEnumerator OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            for (float alpha = evadeTime; alpha >= 0; alpha -= 0.1f)
            {
                agent.SetDestination(target.transform.position);
                chaseMode = false;
                yield return null;
            }
    }
    
    void Update()
    {
        if (patrolMode)
        {
            Transform waypoint = waypoints[currentWaypoint];
            if (Vector3.Distance(obj.position, waypoint.position) < 1f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            }
            else
            {
                agent.SetDestination(waypoint.position);
            }
        }
        if(chaseMode)
            agent.SetDestination(target.transform.position);
        else
        {
            if (waitingTime >= waitTime)
            {
                agent.SetDestination(originalPosition);
                waitingTime = 0f;
            }
            waitingTime += Time.deltaTime;
        }

        if (Vector3.Distance(obj.position, originalPosition) < 1f)
            patrolMode = true;
    }
}
