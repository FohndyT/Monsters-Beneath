// Fohndy Nomerth Tah

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
    [SerializeField] private AnimatorController animationSauter;
    [SerializeField] private AnimatorController animationTomber;
    [SerializeField] private AnimatorController animationEsquiveGauche;
    [SerializeField] private AnimatorController animationEsquiveDroite;
    [SerializeField] private AnimatorController animationFrappe;
    [SerializeField] private AnimatorController animationCréationSlime;
    [SerializeField] private AnimatorController animationPerte;
    [SerializeField] private AnimatorController animationChangementPhase;
    [SerializeField] private AnimatorController animationMort;

    
    [SerializeField] private GameObject slime;
    private GameObject ennemiSlime;
    private bool slimeEstCréer;
    
    [SerializeField] private float vitesseMouvement = 5f;

    private GameObject joueur;
    private Animator animation;

    private float temps;
    
    private bool marche = true;
    private bool attaque;

    
    [NonSerialized] public short phase = 0;
    
    void Start()
    { 
        joueur = GameObject.FindWithTag("Player");
        animation = GetComponent<Animator>();
    }

    void Update()
    {
        RegarderJoueur();
        
        if (phase == 1)
        {
            PoursuitePhaseUn();
        }

        if (phase == 2)
        {
            PoursuitePhaseDeux();
        }
    }

    void RegarderJoueur()
    {
        if (marche)
        {
            transform.LookAt(new Vector3(joueur.transform.position.x,transform.position.y,joueur.transform.position.z));
        }
    }

    void PoursuitePhaseUn()
    {
        if (marche)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(joueur.transform.position.x, transform.position.y, joueur.transform.position.z), vitesseMouvement * Time.deltaTime);
            
            animation.runtimeAnimatorController = animationMarche;
        }
    }
    void PoursuitePhaseDeux()
    {
        if (marche)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(joueur.transform.position.x, transform.position.y, joueur.transform.position.z), vitesseMouvement * 2 * Time.deltaTime);
        
            animation.runtimeAnimatorController = animationCourir;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger Activated");
            marche = false;
            animation.runtimeAnimatorController = animationFrappe;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            marche = true;
        }
    }

    void Esquiver()
    {
        
    }

    void Bouclier()
    {
        
    }

    void Dash()
    {
        
    }

    void CréationSlimesVolants()
    {
        ennemiSlime = Instantiate(slime, transform.position, Quaternion.LookRotation(joueur.transform.position));
    }

    void RochesMontantes()
    {
        
    }

    void AttaqueÀDistance()
    {
        
    }
}
