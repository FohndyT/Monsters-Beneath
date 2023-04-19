using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private Rigidbody projectile;
    private Transform positionAttack;
    private float lifeTime = 5f;
    private float timeSpent = 0f;
    private void Awake()
    {
        projectile = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        projectile.AddForce(projectile.transform.forward, ForceMode.Impulse);
        positionAttack = transform.parent;
        Destroy(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contacts = collision.contacts;
        ContactPoint firstContact = contacts[0];
        Collider thisCollider = firstContact.thisCollider;
        if (firstContact.otherCollider.gameObject.layer != 7)
        {
            Destroy(thisCollider.gameObject);
        }
    }
}
