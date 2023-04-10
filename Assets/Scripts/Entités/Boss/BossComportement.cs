// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private AnimatorController animationCreationSlime;
    [SerializeField] private AnimatorController animationPerte;
    [SerializeField] private AnimatorController animationChangementPhase;
    [SerializeField] private AnimatorController animationMort;

    public BarDeVie barDeVie;
    
    [SerializeField] private GameObject slime;
    private GameObject ennemiSlime;
    private bool slimeEstCreer;

    [SerializeField] public int vieMax = 500;
    [SerializeField] public int vieRestante;
    [SerializeField] private float vitesseMouvement = 5f;

    private GameObject joueur;
    private Animator animation;

    private bool marche = true;
    private bool seFaitAttaquer;

    // Ne pas enlever le NonSerialized
    [NonSerialized] public short phase = 0;
    
    void Start()
    { 
        joueur = GameObject.FindWithTag("Player");
        animation = GetComponent<Animator>();

        vieRestante = vieMax;
        barDeVie.MettreVieMax(vieMax);
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

        if (Input.GetKeyDown("e"))
        {
            PrendreDegats(20);
        }
    }

    void PrendreDegats(int degats)
    {
        vieRestante -= degats;
        barDeVie.MettreVie(vieRestante);
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
            Invoke("MarcheEnTrue",1f);
        }
    }
    private void MarcheEnTrue()
    {
        marche = true;
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

    void CreationSlimesVolants()
    {
        Random.Range(0, 100);
        ennemiSlime = Instantiate(slime, transform.position, Quaternion.LookRotation(joueur.transform.position));
    }

    void RochesMontantes()
    {
        
    }

    void AttaqueADistance()
    {
        
    }
}
