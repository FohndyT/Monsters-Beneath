using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class BossComportement : MonoBehaviour
{
    [SerializeField] private AnimatorController animationDefaut;
    [SerializeField] private AnimatorController animationMarche;
    [SerializeField] private AnimatorController animationCourir;
    [SerializeField] private AnimatorController animationFrappe;
    [SerializeField] private AnimatorController animationChangementPhase;

    private GameObject joueur;
    private Animator animation;

    [SerializeField] private float vitesseMouvement = 5f;
    
    [NonSerialized] public short phase = 1;
    
    void Start()
    { 
        joueur = GameObject.FindWithTag("Player");
        animation = GetComponent<Animator>();
    }

    void Update()
    {
        if (phase == 1)
        {
            PoursuitePhaseUn();
        }

        if (phase == 2)
        {
            PoursuitePhaseDeux();
        }
        
        if (Input.GetKey("b"))
        {
            animation.runtimeAnimatorController = animationMarche;
        }
    }

    void PoursuitePhaseUn()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                                                  new Vector3(joueur.transform.position.x,
                                                                   transform.position.y,
                                                                   joueur.transform.position.z),
                                          vitesseMouvement * Time.deltaTime);
        animation.runtimeAnimatorController = animationMarche;
    }

    void PoursuitePhaseDeux()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(joueur.transform.position.x,
                transform.position.y,
                joueur.transform.position.z),
            vitesseMouvement * 2 * Time.deltaTime);
        animation.runtimeAnimatorController = animationCourir;
    }
    
    
}
