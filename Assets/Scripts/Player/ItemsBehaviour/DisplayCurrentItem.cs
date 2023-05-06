using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DisplayCurrentItem : MonoBehaviour
{
    public GameObject Current_item;
    InputsManager inputs;
    private TextMeshProUGUI textMeshPro_Current_item;
    private int CurrentSpecialId;

    private void Awake()
    {
        inputs = GameObject.Find("Player").GetComponent<InputsManager>();
        textMeshPro_Current_item = Current_item.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        CurrentSpecialId = inputs.selectedItem;
        if (CurrentSpecialId == -1)
            return;
        textMeshPro_Current_item.text = inputs.Items[CurrentSpecialId].name;
    }
}
