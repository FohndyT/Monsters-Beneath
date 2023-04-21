using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        CurrentSpecialId = inputs.itemIndex;
        textMeshPro_Current_item.text = inputs.Items[CurrentSpecialId].name;
    }
}
