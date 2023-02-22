using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackInput : MonoBehaviour
{
    [SerializeField] private GameObject Fouet;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private GameObject PowerGlove;
    [SerializeField] private GameObject CrystalLight;
    [SerializeField] private Transform positionAttack;
    private int CurrentSpecialId = 0;
    private GameObject[] CurrentSpecial;

    private void Awake()
    {
        CurrentSpecial = new[] { Fouet, Projectile, PowerGlove, CrystalLight };
    }

    private void OnSpecialAttack()
    {
        Instantiate(CurrentSpecial[CurrentSpecialId], positionAttack);
    }
    private void OnSwitchSpecialAttack()
    {
        if (CurrentSpecialId < CurrentSpecial.Length-1)
            CurrentSpecialId += 1;
        else
            CurrentSpecialId = 0;
    }
}
