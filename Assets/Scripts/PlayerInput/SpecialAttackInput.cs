using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialAttackInput : MonoBehaviour
{
    [SerializeField] private GameObject Fouet;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private GameObject PowerGlove;
    [SerializeField] private GameObject CrystalLight;
    [SerializeField] private Transform positionAttack;
    public static int CurrentSpecialId = 0;
    public static GameObject[] CurrentSpecial;
    private float cooldown = 0f;
    public bool CanAttack = true;
    public static bool isUsingLight = false;
    

    private void Awake()
    {
        CurrentSpecial = new[] { Fouet, Projectile, PowerGlove, CrystalLight };
    }

    private void OnSpecialAttack()
    {
        if (CanAttack)
        {
            if (CurrentSpecialId == 3)
            {
                if (isUsingLight)
                {
                    isUsingLight = false;
                    return;
                }
                Instantiate(CrystalLight, positionAttack);
                isUsingLight = true;
                return;
            }
            if (CurrentSpecialId == 1)
            {
                Instantiate(Projectile, positionAttack);
                positionAttack.transform.DetachChildren();
            }
            else
                Instantiate(CurrentSpecial[CurrentSpecialId], positionAttack);
        }
        CanAttack = false;
    }
    private void OnSwitchSpecialAttack()
    {
        if (isUsingLight)
            return;
        if (CurrentSpecialId < CurrentSpecial.Length-1)
            CurrentSpecialId += 1;
        else
            CurrentSpecialId = 0;
    }

    private void Update()
    {
        if(!CanAttack)
            if (cooldown < 0.75f)
                cooldown += Time.deltaTime;
            else
            {
                CanAttack = true;
                cooldown = 0; 
            }

        
    }
}
