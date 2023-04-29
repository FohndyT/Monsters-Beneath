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

    [SerializeField] public int vieMax = 500;
    [SerializeField] public int vieRestante;
    [SerializeField] private float vitesseMouvement = 5f;

    private GameObject joueur;
    private Animator animation;

    private bool marche = true;
    // private bool seFaitAttaquer;
    private bool dashEnMarche;
    // private bool slimeEstCreer;

    private Rigidbody rb;
    
    private float temps;
    private Vector3 positionJoueurDash;
    private Vector3 directionJoueurDash;

    // Ne pas enlever le NonSerialized
    [NonSerialized] public short phase = 0;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joueur = GameObject.FindWithTag("Player");
        animation = GetComponent<Animator>();

        vieRestante = vieMax;
        barDeVie.MettreVieMax(vieMax);

        StartCoroutine(Attente());
    }

    void Update()
    {
        RegarderJoueur();
        
        if (phase == 1)
        {
            PoursuitePhaseUn();
            Dash();
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

    // Attaque en corps à corps
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
            // Ne pas changer
            Invoke("MarcheEnTrue",1f);
        }
    }
    private void MarcheEnTrue()
    {
        marche = true;
    }

    IEnumerator Attente()
    {
        // Dash
        while (true)
        {
            yield return new WaitForSeconds(10);

            float nombre = Random.Range(0, 5);
            if (nombre == 0 && marche && !dashEnMarche)
            {
                dashEnMarche = true;
                positionJoueurDash = joueur.transform.position;
                positionJoueurDash = (transform.position - positionJoueurDash).normalized;

            }
        }
    }

    void Bouclier()
    {
        
    }

    void Dash()
    {
        if (dashEnMarche)
        {
            temps += Time.deltaTime;
            
            marche = false;
            
            //transform.position = Vector3.MoveTowards(transform.position, direction,vitesseMouvement * Time.deltaTime);
            
            // transform.position += vitesseMouvement / 70f * temps * -positionJoueurDash;
            
            transform.position += vitesseMouvement / 30f * -positionJoueurDash;
            
            animation.runtimeAnimatorController = animationCourir;

            Invoke("MarcheEnTrue",4f);
            Invoke("MettreDashEnFalse", 4f);
        }
    }

    void MettreDashEnFalse()
    {
        dashEnMarche = false;
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
