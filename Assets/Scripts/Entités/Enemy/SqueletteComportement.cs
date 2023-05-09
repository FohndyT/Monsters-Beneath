using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEditor.Animations;
using UnityEngine;

public class SqueletteComportement : MonoBehaviour
{
    [SerializeField] private AnimatorController animationRepos;
    [SerializeField] private AnimatorController animationMarcher;
    [SerializeField] private AnimatorController animationAttaquer;
    
    private Animator animation;
    
    private GameObject joueur;
    private Player joueurPlayer;

    private bool peutAttaquer = true;
    private bool joueurPeutAttaquer = true;
    private bool estEnTrainDeMarcher = true;

    private float vie = 3;
    
    void Start()
    {
        animation = GetComponent<Animator>();
        animation.runtimeAnimatorController = animationRepos;
        
        joueur = GameObject.Find("Player");
        joueurPlayer = joueur.GetComponent<Player>();
    }

    void Update()
    {
        SeDeplacer();
        
        if (vie == 0)
        {
            Destroy(gameObject);
        }
    }

    void Attaquer()
    {
        animation.runtimeAnimatorController = animationAttaquer;
    }

    void SeDeplacer()
    {
        if (estEnTrainDeMarcher && Vector3.Distance(joueur.transform.position, gameObject.transform.position) < 15)
        {
            transform.LookAt(new Vector3(joueur.transform.position.x,transform.position.y,joueur.transform.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(joueur.transform.position.x, transform.position.y, joueur.transform.position.z), 5f * Time.deltaTime);
            animation.runtimeAnimatorController = animationMarcher;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && peutAttaquer)
        {
            estEnTrainDeMarcher = false;
            Attaquer();
        }

        if (other.CompareTag("PlayerAttack") && joueurPeutAttaquer)
        {
            vie--;
            joueurPeutAttaquer = false;
            this.Attendre(1f, () => { joueurPeutAttaquer = true;});
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.Attendre(2f, ()=> {estEnTrainDeMarcher = true;});
    }
}
