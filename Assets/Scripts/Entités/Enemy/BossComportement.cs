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
    private Transform positionYJoueur;

    private GameObject joueur;
    private Animator animation;

    
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
        // positionYJoueur.position = new Vector3(joueur.transform.position.x, transform.position.y, joueur.transform.position.z);
        
        // Faire en sorte que si le boss attaque, que son celle-ci ne suit pas le joueur. À enlever si fait.
        //transform.LookAt(positionYJoueur);
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
            new Vector3(joueur.transform.position.x, transform.position.y, joueur.transform.position.z), vitesseMouvement * 2 * Time.deltaTime);
        animation.runtimeAnimatorController = animationCourir;
    }

    private void OnCollisionEnter(Collision joueur)
    {
        
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
