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
    [SerializeField] private Transform handPos;
    public static int itemIndex = 0;
    public static GameObject[] Items;
    private float cooldown = 0f;
    public bool canAttack = true;
    public static bool isUsingLight = false;


    private void Awake()
    {
        Items = new[] { Fouet, Projectile, PowerGlove, CrystalLight };
    }

    private void OnSpecialAttack()
    {
        if (canAttack)
        {
            if (itemIndex == 3)
            {
                if (isUsingLight)
                {
                    isUsingLight = false;
                    return;
                }
                Instantiate(CrystalLight, handPos);
                isUsingLight = true;
                return;
            }
            if (itemIndex == 1)
            {
                Instantiate(Projectile, handPos);
                handPos.transform.DetachChildren();
            }
            else
                Instantiate(Items[itemIndex], handPos);
        }
        canAttack = false;
    }
    private void OnSwitchSpecialAttack()
    {
        if (isUsingLight)
            return;
        if (itemIndex < Items.Length - 1)
            itemIndex += 1;
        else
            itemIndex = 0;
    }

    private void Update()
    {
        if (!canAttack)
        {
            if (cooldown < 0.75f)
                cooldown += Time.deltaTime;
            else
            {
                canAttack = true;
                cooldown = 0;
            }
        }



    }
}
