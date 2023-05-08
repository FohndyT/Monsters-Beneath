// Fohndy Nomerth Tah

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    [SerializeField] private GameObject ennemi;

    private DommageBoss attaqueCorps;
    private DommageBoss attaqueMainGauche;
    private DommageBoss attaqueMainDroite;
    private DommageBoss attaquePiedsGauche;

    public BarDeVie barDeVie;
    
    [SerializeField] public int vieMax = 500;
    private int vieRestante;
    [SerializeField] private float vitesseMouvement = 5f;

    private Rigidbody rb;
    private Animator animation;
    private GameObject joueur;
    private Rigidbody rbJoueur;

    private bool peutAttaquer = true;
    private bool EstEnTrainDeMarcher = true;
    private bool peutGenererOnde = true;
    
    private float temps;
    private Vector3 positionJoueurDash;
    private Vector3 directionJoueurDash;

    // Ne pas enlever le NonSerialized
    [NonSerialized] public short phase = 0;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joueur = GameObject.Find("Player");
        rbJoueur = joueur.GetComponent<Rigidbody>();
        animation = GetComponent<Animator>();

        vieRestante = vieMax;
        barDeVie.MettreVieMax(vieMax);

        attaqueCorps = GameObject.Find("Boss").GetComponent<DommageBoss>();
        attaqueMainGauche = GameObject.Find("B-hand.L").GetComponent<DommageBoss>();
        attaqueMainDroite = GameObject.Find("B-hand.R").GetComponent<DommageBoss>();
        attaquePiedsGauche = GameObject.Find("B-toe.L").GetComponent<DommageBoss>();
    }

    void FixedUpdate()
    {
        RegarderJoueur();
        
        if (phase == 1)
        {
            PoursuitePhaseUn();
            OndeDeChoc();
        }

        if (phase == 2)
        {
            PoursuitePhaseDeux();
            OndeDeChoc();
        }

        if (vieRestante <= 225f)
        {
            ChangementPhase2();
        }
        
        if (vieRestante <= 0)
        {
            Meurt();
        }
        
        // À enlever après
        if (Input.GetKeyDown("h"))
        {
            PrendreDegats(100);
        }
    }

    #region Mouvements
    void RegarderJoueur()
    {
        if (EstEnTrainDeMarcher)
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
        if (EstEnTrainDeMarcher)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(joueur.transform.position.x, transform.position.y, joueur.transform.position.z), vitesseMouvement * Time.deltaTime);
            
            animation.runtimeAnimatorController = animationMarche;
        }
    }
    void PoursuitePhaseDeux()
    {
        if (EstEnTrainDeMarcher)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(joueur.transform.position.x, transform.position.y, joueur.transform.position.z), vitesseMouvement * 2f * Time.deltaTime);
        
            animation.runtimeAnimatorController = animationCourir;
        }
    }
    #endregion

    #region AttaqueCorpsACorps
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && peutAttaquer)
        {
            EstEnTrainDeMarcher = false;
            
            attaqueMainGauche.estActive = true;
            attaqueMainDroite.estActive = true;
            attaquePiedsGauche.estActive = true;

            animation.runtimeAnimatorController = animationFrappe;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && peutAttaquer)
        {
            this.Attendre(2f, ()=> {EstEnTrainDeMarcher = true;});
            
            attaqueMainGauche.estActive = false;
            attaqueMainDroite.estActive = false;
            attaquePiedsGauche.estActive = false;
        }
    }
    #endregion

    #region Attaques
    void OndeDeChoc()
    {
        if (DistanceEntreBossJoueur() <= 7f && peutGenererOnde && peutAttaquer)
        {
            EstEnTrainDeMarcher = false;

            animation.runtimeAnimatorController = animationCreationSlime;
            this.Attendre(2f, () =>
            {
                GameObject cloneOndeDeChoc = Instantiate(ondeDeShoc,transform.position,transform.rotation);
                Vector3 directionForce = new Vector3((joueur.transform.position - transform.position).x,500f/4000f,(joueur.transform.position - transform.position).z);
                if (DistanceEntreBossJoueur() <= 7f)
                {
                    rbJoueur.AddForce(4000f * directionForce, ForceMode.Impulse);
                }
                this.Attendre(1.7f, () => { Destroy(cloneOndeDeChoc);});
            });

            peutGenererOnde = false;
            this.Attendre(4f, () => { EstEnTrainDeMarcher = true;});
            this.Attendre(20f, () => { peutGenererOnde = true;});
        }
    }
    #endregion
    
    public void PrendreDegats(int degats)
    {
        vieRestante -= degats;
        barDeVie.MettreVie(vieRestante);
    }
    void ChangementPhase2()
    {
        if (phase == 1)
        {
            peutAttaquer = false;
            EstEnTrainDeMarcher = false;
            animation.runtimeAnimatorController = animationPerte;
            this.Attendre(5f, () => { EstEnTrainDeMarcher = true; peutAttaquer = true;});
            phase = 2;
        }
    }
    void Meurt()
    {
        EstEnTrainDeMarcher = false;
        peutAttaquer = false;
        vieRestante = 0;
        animation.runtimeAnimatorController = animationMort;
        this.Attendre(2.4f, () => { Destroy(gameObject);});
    }
    private float DistanceEntreBossJoueur()
    {
        return Vector3.Distance(transform.position, joueur.transform.position);
    }
}
