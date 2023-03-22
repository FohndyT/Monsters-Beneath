using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCurrentItem : MonoBehaviour
{
    public GameObject Current_item;
    private TextMeshProUGUI textMeshPro_Current_item;
    private int CurrentSpecialId;

    private void Awake()
    {
        textMeshPro_Current_item = Current_item.GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        CurrentSpecialId = SpecialAttackInput.CurrentSpecialId;
        textMeshPro_Current_item.text = SpecialAttackInput.CurrentSpecial[CurrentSpecialId].name;
    }
}
