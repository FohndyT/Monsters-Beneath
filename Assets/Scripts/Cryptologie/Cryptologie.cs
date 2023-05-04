using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cryptologie : MonoBehaviour
{
    [SerializeField]
    public TMP_Text buttonTextNonCrypted1;
    public TMP_Text buttonTextNonCrypted2;
    public TMP_Text buttonTextNonCrypted3;
    public TMP_Text buttonTextNonCrypted4;
    public TMP_Text buttonTextNonCrypted5;
    public TMP_Text buttonTextNonCrypted6;
    public TMP_Text buttonTextNonCrypted7;
    public TMP_Text buttonTextCrypted1;
    public TMP_Text buttonTextCrypted2;
    public TMP_Text buttonTextCrypted3;
    public TMP_Text buttonTextCrypted4;
    public TMP_Text buttonTextCrypted5;
    public TMP_Text buttonTextCrypted6;
    public TMP_Text buttonTextCrypted7;

    private string[] lettres = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    public int[] IndiceDeLettreParBoutons = new int[14];

    public void AvancerDUneLettre(int indice, TMP_Text button)
    {
        IndiceDeLettreParBoutons[indice] = (IndiceDeLettreParBoutons[indice] + 1) % lettres.Length;
        button.text = lettres[IndiceDeLettreParBoutons[indice]];
    }
    public void CrypterUneLettre(int indice, TMP_Text button)
    {
        IndiceDeLettreParBoutons[indice] = (IndiceDeLettreParBoutons[indice] + 3) % lettres.Length;
        button.text = lettres[IndiceDeLettreParBoutons[indice]];
    }
    public void OnClickBouttonNonCrypted1()
    {
        AvancerDUneLettre(0, buttonTextNonCrypted1);
        CrypterUneLettre(7, buttonTextCrypted1);
    }
    public void OnClickBouttonNonCrypted2()
    {
        AvancerDUneLettre(1, buttonTextNonCrypted2);
        CrypterUneLettre(8, buttonTextCrypted2);
    }
    public void OnClickBouttonNonCrypted3()
    {
        AvancerDUneLettre(2, buttonTextNonCrypted3);
        CrypterUneLettre(9, buttonTextCrypted3);
    }
    public void OnClickBouttonNonCrypted4()
    {
        AvancerDUneLettre(3, buttonTextNonCrypted4);
        CrypterUneLettre(10, buttonTextCrypted4);
    }
    public void OnClickBouttonNonCrypted5()
    {
        AvancerDUneLettre(4, buttonTextNonCrypted5);
        CrypterUneLettre(11, buttonTextCrypted5);
    }
    public void OnClickBouttonNonCrypted6()
    {
        AvancerDUneLettre(5, buttonTextNonCrypted6);
        CrypterUneLettre(12, buttonTextCrypted6);
    }
    public void OnClickBouttonNonCrypted7()
    {
        AvancerDUneLettre(6, buttonTextNonCrypted7);
        CrypterUneLettre(13, buttonTextCrypted7);
    }
}
