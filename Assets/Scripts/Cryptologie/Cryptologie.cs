using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cryptologie : MonoBehaviour
{
    [SerializeField]
    public TMP_Text buttonTextNonCrypté1;
    public TMP_Text buttonTextNonCrypté2;
    public TMP_Text buttonTextNonCrypté3;
    public TMP_Text buttonTextNonCrypté4;
    public TMP_Text buttonTextNonCrypté5;
    public TMP_Text buttonTextNonCrypté6;
    public TMP_Text buttonTextNonCrypté7;
    public TMP_Text buttonTextCrypté1;
    public TMP_Text buttonTextCrypté2;
    public TMP_Text buttonTextCrypté3;
    public TMP_Text buttonTextCrypté4;
    public TMP_Text buttonTextCrypté5;
    public TMP_Text buttonTextCrypté6;
    public TMP_Text buttonTextCrypté7;

    private string[] lettres = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    public int[] IndiceDeLettreParBoutons = new int[14];

    public void AvancerDUneLettre (int indice)
    {
        IndiceDeLettreParBoutons[indice] = (IndiceDeLettreParBoutons[indice] + 1) % lettres.Length;
        buttonTextNonCrypté1.text = lettres[IndiceDeLettreParBoutons[indice]];
    }
    public void CrypterUneLettre(int indice)
    {
        IndiceDeLettreParBoutons[indice] = (IndiceDeLettreParBoutons[indice] + 3) % lettres.Length;
        buttonTextCrypté1.text = lettres[IndiceDeLettreParBoutons[indice]];
    }
    public void OnClickBouttonNonCrypté1()
    {
        AvancerDUneLettre(0);
        CrypterUneLettre(7);
    }
    public void OnClickBouttonNonCrypté2()
    {
        AvancerDUneLettre(1);
        CrypterUneLettre(8);
    }
    public void OnClickBouttonNonCrypté3()
    {
        AvancerDUneLettre(2);
        CrypterUneLettre(9);
    }
    public void OnClickBouttonNonCrypté4()
    {
        AvancerDUneLettre(3);
        CrypterUneLettre(10);
    }
    public void OnClickBouttonNonCrypté5()
    {
        AvancerDUneLettre(4);
        CrypterUneLettre(11);
    }
    public void OnClickBouttonNonCrypté6()
    {
        AvancerDUneLettre(5);
        CrypterUneLettre(12);
    }
    public void OnClickBouttonNonCrypté7()
    {
        AvancerDUneLettre(6);
        CrypterUneLettre(13);
    }
}
