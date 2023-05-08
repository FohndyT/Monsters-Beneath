// Fohndy Nomerth Tah et Jeremy Legault

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Planage : MonoBehaviour
{
    [SerializeField] private GameObject planeur;
    [NonSerialized] public float vitesseParachute;
    private GameObject clonePlaneur;
    private Rigidbody rb;

    public bool collectedGlider;
    [NonSerialized] public bool instantiated;
    bool inUse;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        vitesseParachute = -2f;
    }

    private void Update()
    {
        MettreVitesse();
    }
    void OnGlider(InputValue value)
    {
        inUse = !inUse;

        if (collectedGlider)
        {
            if (!inUse && !instantiated)
            {
                clonePlaneur = Instantiate(planeur, transform.localPosition + new Vector3(0, 0.75f, 0), transform.rotation);
                clonePlaneur.transform.localScale = Vector3.one;
                clonePlaneur.transform.parent = transform;

                instantiated = true;
            }

            if (inUse && instantiated)
            {
                Destroy(clonePlaneur);
                instantiated = false;
            }
        }
    }
    private void MettreVitesse()
    {
        if (instantiated)
        {
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, vitesseParachute, rb.velocity.z);
        }
        else
        {
            rb.useGravity = true;
        }
    }
}
