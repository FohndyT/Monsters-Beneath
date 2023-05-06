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

    [SerializeField] private GameObject ondeDeShoc;

    private DommageBoss attaqueCorps;
    private DommageBoss attaqueMainGauche;
    private DommageBoss attaqueMainDroite;
    private DommageBoss attaquePiedsGauche;

    public BarDeVie barDeVie;
    
    [SerializeField] public int vieMax = 500;
    private int vieRestante;
    [SerializeField] private float vitesseMouvement = 5f;

    private GameObject joueur;
    private Animator animation;

    private bool estProchePourSaut;
    private bool marche = true;
    private bool sautEnMarche;
    private bool RochesMontantesEnMarche;

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
    }

    void Update()
    {
        RegarderJoueur();
        
        if (phase == 1)
        {
            PoursuitePhaseUn();
            Sauter();
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
    void TournerVersJoueur()
    {
        transform.LookAt(new Vector3(joueur.transform.position.x,transform.position.y,joueur.transform.position.z));
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
        if (other.gameObject.name == "Player" && sautEnMarche == false)
        {
            Debug.Log("Trigger Activated");
            marche = false;
            estProchePourSaut = true;
            
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
            this.Attendre(2f, () => { marche = true;});
            
            attaqueMainGauche.estActive = false;
            attaqueMainDroite.estActive = false;
            attaquePiedsGauche.estActive = false;

            estProchePourSaut = false;
        }
    }
    #endregion

    #region Attaques
    void Sauter()
    {
        if (estProchePourSaut)
        {
            this.Attendre(7f, () =>
            {
                if (estProchePourSaut)
                {
                    sautEnMarche = true;
                    marche = false;
                    
                    animation.runtimeAnimatorController = animationSauter;
                    rb.AddForce(new Vector3(0,15000f,0),ForceMode.Impulse);
                    
                    
                    this.Attendre(3f, () => { GameObject cloneOnde = Instantiate(ondeDeShoc,transform.position,transform.rotation); this.Attendre(1f, ()=> Destroy(cloneOnde));});
                    this.Attendre(4f, () => { sautEnMarche = false;});
                    this.Attendre(4f, () => { marche = true;});
                }
            });
        }
    }
    #endregion
    
    void MettreRochesMontantesEnFalse()
    {
        RochesMontantesEnMarche = false;
    }
    void MettreSautEnFalse()
    {
        sautEnMarche = false;
        attaqueCorps.estActive = false;
    }
    private void MarcheEnTrue()
    {
        marche = true;
    }
}
