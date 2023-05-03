using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cryptologie : MonoBehaviour
{
    [SerializeField]
    public TMP_Text buttonTextNonCrypte1;
    public TMP_Text buttonTextNonCrypte2;
    public TMP_Text buttonTextNonCrypte3;
    public TMP_Text buttonTextNonCrypte4;
    public TMP_Text buttonTextNonCrypte5;
    public TMP_Text buttonTextNonCrypte6;
    public TMP_Text buttonTextNonCrypte7;
    public TMP_Text buttonTextCrypte1;
    public TMP_Text buttonTextCrypte2;
    public TMP_Text buttonTextCrypte3;
    public TMP_Text buttonTextCrypte4;
    public TMP_Text buttonTextCrypte5;
    public TMP_Text buttonTextCrypte6;
    public TMP_Text buttonTextCrypte7;

    private string[] lettres = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    public int[] IndiceDeLettreParBoutons = new int[14];

    public void AvancerDUneLettre (int indice)
    {
        IndiceDeLettreParBoutons[indice] = (IndiceDeLettreParBoutons[indice] + 1) % lettres.Length;
        buttonTextNonCrypte1.text = lettres[IndiceDeLettreParBoutons[indice]];
    }
    public void CrypterUneLettre(int indice)
    {
        IndiceDeLettreParBoutons[indice] = (IndiceDeLettreParBoutons[indice] + 3) % lettres.Length;
        buttonTextCrypte1.text = lettres[IndiceDeLettreParBoutons[indice]];
    }
    public void OnClickBouttonNonCrypte1()
    {
        AvancerDUneLettre(0);
        CrypterUneLettre(7);
    }
    public void OnClickBouttonNonCrypte2()
    {
        AvancerDUneLettre(1);
        CrypterUneLettre(8);
    }
    public void OnClickBouttonNonCrypte3()
    {
        AvancerDUneLettre(2);
        CrypterUneLettre(9);
    }
    public void OnClickBouttonNonCrypte4()
    {
        AvancerDUneLettre(3);
        CrypterUneLettre(10);
    }
    public void OnClickBouttonNonCrypte5()
    {
        AvancerDUneLettre(4);
        CrypterUneLettre(11);
    }
    public void OnClickBouttonNonCrypte6()
    {
        AvancerDUneLettre(5);
        CrypterUneLettre(12);
    }
    public void OnClickBouttonNonCrypte7()
    {
        AvancerDUneLettre(6);
        CrypterUneLettre(13);
    }
}
