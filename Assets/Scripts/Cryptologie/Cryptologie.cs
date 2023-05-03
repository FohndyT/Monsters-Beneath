using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cryptologie : MonoBehaviour
{
    [SerializeField]
    public TMP_Text buttonTextNonCrypt�1;
    public TMP_Text buttonTextNonCrypt�2;
    public TMP_Text buttonTextNonCrypt�3;
    public TMP_Text buttonTextNonCrypt�4;
    public TMP_Text buttonTextNonCrypt�5;
    public TMP_Text buttonTextNonCrypt�6;
    public TMP_Text buttonTextNonCrypt�7;
    public TMP_Text buttonTextCrypt�1;
    public TMP_Text buttonTextCrypt�2;
    public TMP_Text buttonTextCrypt�3;
    public TMP_Text buttonTextCrypt�4;
    public TMP_Text buttonTextCrypt�5;
    public TMP_Text buttonTextCrypt�6;
    public TMP_Text buttonTextCrypt�7;

    private string[] lettres = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    public int[] IndiceDeLettreParBoutons = new int[14];

    public void AvancerDUneLettre (int indice)
    {
        IndiceDeLettreParBoutons[indice] = (IndiceDeLettreParBoutons[indice] + 1) % lettres.Length;
        buttonTextNonCrypt�1.text = lettres[IndiceDeLettreParBoutons[indice]];
    }
    public void CrypterUneLettre(int indice)
    {
        IndiceDeLettreParBoutons[indice] = (IndiceDeLettreParBoutons[indice] + 3) % lettres.Length;
        buttonTextCrypt�1.text = lettres[IndiceDeLettreParBoutons[indice]];
    }
    public void OnClickBouttonNonCrypt�1()
    {
        AvancerDUneLettre(0);
        CrypterUneLettre(7);
    }
    public void OnClickBouttonNonCrypt�2()
    {
        AvancerDUneLettre(1);
        CrypterUneLettre(8);
    }
    public void OnClickBouttonNonCrypt�3()
    {
        AvancerDUneLettre(2);
        CrypterUneLettre(9);
    }
    public void OnClickBouttonNonCrypt�4()
    {
        AvancerDUneLettre(3);
        CrypterUneLettre(10);
    }
    public void OnClickBouttonNonCrypt�5()
    {
        AvancerDUneLettre(4);
        CrypterUneLettre(11);
    }
    public void OnClickBouttonNonCrypt�6()
    {
        AvancerDUneLettre(5);
        CrypterUneLettre(12);
    }
    public void OnClickBouttonNonCrypt�7()
    {
        AvancerDUneLettre(6);
        CrypterUneLettre(13);
    }
}
