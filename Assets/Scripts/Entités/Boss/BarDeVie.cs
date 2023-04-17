// Fohndy Nomerth Tah

// https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarDeVie : MonoBehaviour
{
    public Slider slider;
    public Gradient gradiant;
    public Image Vie;

    public Transform caméra;

    public void MettreVieMax(int vieMax)
    {
        slider.maxValue = vieMax;
        slider.value = vieMax;

        Vie.color = gradiant.Evaluate(1f);
        //transform.GetChild(0).GetComponent<Image>().color = gradiant.Evaluate(1f);
    }

    public void MettreVie(int vieCurrente)
    {
        slider.value = vieCurrente;
        
        Vie.color = gradiant.Evaluate(slider.normalizedValue);
    }

    private void LateUpdate()
    {
        transform.LookAt(caméra);
    }
}
