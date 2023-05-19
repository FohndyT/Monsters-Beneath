//Imane
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
    private int[] IndiceDeLettreParBoutons = new int[14];

    public string AvancerDUneLettre(int indice, TMP_Text button)
    {
        IndiceDeLettreParBoutons[indice] = (IndiceDeLettreParBoutons[indice] + 1) % lettres.Length;
        return lettres[IndiceDeLettreParBoutons[indice]];
    }
    public void AvancerDuneLettre2 ( TMP_Text button)
    {
        char buttonChar = (char)(((button.text.ToCharArray()[0] - 'a') + 1) % 26 + 'a');
        button.text = buttonChar.ToString();
    }
    public void CrypterUneLettre(int indice, TMP_Text button)
    {
        IndiceDeLettreParBoutons[indice] = (IndiceDeLettreParBoutons[indice] + 3) % lettres.Length;
        button.text= lettres[IndiceDeLettreParBoutons[indice]];
    }
    public void CrypterUneLettre2( TMP_Text button1, TMP_Text button2)
    {
       char buttonChar =(char) (((button1.text.ToCharArray()[0] -'a') + 3) % 26 + 'a') ;
        button2.text = buttonChar.ToString();
    }
    public void OnClickBouttonNonCrypted1()
    {
        AvancerDuneLettre2(buttonTextNonCrypted1);
        CrypterUneLettre2(buttonTextNonCrypted1, buttonTextCrypted1);  
    }
    public void OnClickBouttonNonCrypted2()
    {
        AvancerDuneLettre2(buttonTextNonCrypted2);
        CrypterUneLettre2(buttonTextNonCrypted2 , buttonTextCrypted2);
    }
    public void OnClickBouttonNonCrypted3()
    {
        AvancerDuneLettre2(buttonTextNonCrypted3);
        CrypterUneLettre2(buttonTextNonCrypted3, buttonTextCrypted3);
    }
    public void OnClickBouttonNonCrypted4()
    {
        AvancerDuneLettre2( buttonTextNonCrypted4);
        CrypterUneLettre2(buttonTextNonCrypted4, buttonTextCrypted4);
    }
    public void OnClickBouttonNonCrypted5()
    {
        AvancerDuneLettre2(buttonTextNonCrypted5);
        CrypterUneLettre2(buttonTextNonCrypted5, buttonTextCrypted5);
    }
    public void OnClickBouttonNonCrypted6()
    {
        AvancerDuneLettre2(buttonTextNonCrypted6);
        CrypterUneLettre2(buttonTextCrypted6, buttonTextCrypted6);
    }
    public void OnClickBouttonNonCrypted7()
    {
        AvancerDuneLettre2(buttonTextNonCrypted7);
        CrypterUneLettre2(buttonTextNonCrypted7, buttonTextCrypted7);
    }
}
