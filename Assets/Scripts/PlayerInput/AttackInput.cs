using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class AttackInput : MonoBehaviour
{
    [SerializeField] private GameObject Sword;
    [SerializeField] private Transform positionAttack;

    private void OnAttack() => Instantiate(Sword, positionAttack);
    
}
