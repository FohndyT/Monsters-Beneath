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
    #region Animations
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
    #endregion

    private DommageBoss attaqueCorps;
    private DommageBoss attaqueMainGauche;
    private DommageBoss attaqueMainDroite;
    private DommageBoss attaquePiedsGauche;

    public BarDeVie barDeVie;
    
    [SerializeField] private GameObject slime;
    private GameObject ennemiSlime;

    [SerializeField] public int vieMax = 500;
    [NonSerialized] private int vieRestante;
    [SerializeField] private float vitesseMouvement = 5f;

    private GameObject joueur;
    private Animator animation;

    private bool marche = true;
    // private bool seFaitAttaquer;
    private bool dashEnMarche;
    private bool RochesMontantesEnMarche;
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
        joueur = GameObject.Find("Player");
        animation = GetComponent<Animator>();

        vieRestante = vieMax;
        barDeVie.MettreVieMax(vieMax);

        attaqueCorps = GameObject.Find("Boss").GetComponent<DommageBoss>();
        attaqueMainGauche = GameObject.Find("B-hand.L").GetComponent<DommageBoss>();
        attaqueMainDroite = GameObject.Find("B-hand.R").GetComponent<DommageBoss>();
        attaquePiedsGauche = GameObject.Find("B-toe.L").GetComponent<DommageBoss>();

        StartCoroutine(AttenteDash());
        StartCoroutine(AttenteRochesMontantes());
    }

    void Update()
    {
        RegarderJoueur();
        
        if (phase == 1)
        {
            PoursuitePhaseUn();
            //AttaqueCorpsACorps();
            Dash();
        }

        if (phase == 2)
        {
            PoursuitePhaseDeux();
        }
        
        // À enlever après
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

    #region Mouvements
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
            new Vector3(joueur.transform.position.x, transform.position.y, joueur.transform.position.z), vitesseMouvement * 1.5f * Time.deltaTime);
        
            animation.runtimeAnimatorController = animationCourir;
        }
    }
    #endregion

    #region AttaqueCorpsACorps
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Trigger Activated");
            marche = false;
            
            attaqueMainGauche.estActive = true;
            attaqueMainDroite.estActive = true;
            attaquePiedsGauche.estActive = true;
            
            animation.runtimeAnimatorController = animationFrappe;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ne pas changer
            Invoke("MarcheEnTrue",1.5f);
            attaqueMainGauche.estActive = false;
            attaqueMainDroite.estActive = false;
            attaquePiedsGauche.estActive = false;
        }
    }
    #endregion

    #region Attente
    IEnumerator AttenteDash()
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
    IEnumerator AttenteRochesMontantes()
    {
        
    }
    #endregion

    #region Attaques
    void RochesMontantes()
    {
        if (RochesMontantesEnMarche)
        {
            
            
            animation.runtimeAnimatorController = animationSauter;
        }
    }
    void Dash()
    {
        if (dashEnMarche)
        {
            temps += Time.deltaTime;
            
            marche = false;
            
            transform.position += vitesseMouvement / 30f * -positionJoueurDash;

            attaqueCorps.estActive = true;
            
            animation.runtimeAnimatorController = animationCourir;

            Invoke("MarcheEnTrue",4f);
            Invoke("MettreDashEnFalse", 4f);
        }
    }
    #endregion

    #region MettreEnBool
    void MettreRochesMontantesEnFalse()
    {
        RochesMontantesEnMarche = false;
    }
    void MettreDashEnFalse()
    {
        dashEnMarche = false;
        attaqueCorps.estActive = false;
    }
    private void MarcheEnTrue()
    {
        marche = true;
    }
    #endregion
}
