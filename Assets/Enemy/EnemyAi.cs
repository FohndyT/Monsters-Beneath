using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private NavMeshAgent agent;
    private bool chaseMode;
    private Vector3 originalposition;
    [SerializeField] private float evadeTime = 5f;
    [SerializeField] private float waitTime = 5f;
    private float waitingTime = 0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        originalposition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            chaseMode = true;
    }

    private IEnumerator OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            for (float alpha = evadeTime; alpha >= 0; alpha -= 0.1f)
            {
                agent.SetDestination(target.transform.position);
                yield return null;
            }
        chaseMode = false;
    }
    
    void Update()
    {
        if(chaseMode)
            agent.SetDestination(target.transform.position);
        else
        {
            if (waitingTime >= waitTime)
            {
                agent.SetDestination(originalposition);
                waitingTime = 0f;
            }
            waitingTime += Time.deltaTime;
        }
    }
}
