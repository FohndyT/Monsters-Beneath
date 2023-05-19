// Imane
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public delegate void ButtonClickEventHandler(object sender, EventArgs e);

public class CustomButton : Button
{
    public event ButtonClickEventHandler ButtonClick;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (ButtonClick != null)
        {
            ButtonClick(this, EventArgs.Empty);
        }
    }
}

public class CryptologieEventSystem : MonoBehaviour
{
    public InputField inputText;
    public TMP_Text outputText;
    public CustomButton buttonNonCrypted1;
    public CustomButton buttonNonCrypted2;
    public CustomButton buttonNonCrypted3;
    public CustomButton buttonNonCrypted4;
    public CustomButton buttonNonCrypted5;
    public CustomButton buttonNonCrypted6;
    public CustomButton buttonNonCrypted7;
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

    void Start()
    {
        // add event handlers to each button
        buttonNonCrypted1.ButtonClick += OnClickButtonNonCrypted1;
        buttonNonCrypted2.ButtonClick += OnClickButtonNonCrypted2;
        buttonNonCrypted3.ButtonClick += OnClickButtonNonCrypted3;
        buttonNonCrypted4.ButtonClick += OnClickButtonNonCrypted4;
        buttonNonCrypted5.ButtonClick += OnClickButtonNonCrypted5;
        buttonNonCrypted6.ButtonClick += OnClickButtonNonCrypted6;
        buttonNonCrypted7.ButtonClick += OnClickButtonNonCrypted7;
    }
    public void AvancerDuneLettre2(TMP_Text button)
    {
        char buttonChar = (char)(((button.text.ToCharArray()[0] - 'a') + 1) % 26 + 'a');
        button.text = buttonChar.ToString();
    }
    public void CrypterUneLettre2(TMP_Text button1, TMP_Text button2)
    {
        char buttonChar = (char)(((button1.text.ToCharArray()[0] - 'a') + 3) % 26 + 'a');
        button2.text = buttonChar.ToString();
    }
    void OnClickButtonNonCrypted1(object sender, EventArgs e)
    {
        AvancerDuneLettre2(buttonTextNonCrypted1);
        CrypterUneLettre2(buttonTextNonCrypted1, buttonTextCrypted1);
    }

    void OnClickButtonNonCrypted2(object sender, EventArgs e)
    {
        AvancerDuneLettre2(buttonTextNonCrypted2);
        CrypterUneLettre2(buttonTextNonCrypted2, buttonTextCrypted2);
    }

    void OnClickButtonNonCrypted3(object sender, EventArgs e)
    {
        AvancerDuneLettre2(buttonTextNonCrypted3);
        CrypterUneLettre2(buttonTextNonCrypted3, buttonTextCrypted3);
    }

    void OnClickButtonNonCrypted4(object sender, EventArgs e)
    {
        AvancerDuneLettre2(buttonTextNonCrypted4);
        CrypterUneLettre2(buttonTextNonCrypted4, buttonTextCrypted4);
    }

    void OnClickButtonNonCrypted5(object sender, EventArgs e)
    {
        AvancerDuneLettre2(buttonTextNonCrypted5);
        CrypterUneLettre2(buttonTextCrypted5, buttonTextCrypted5);
    }

    void OnClickButtonNonCrypted6(object sender, EventArgs e)
    {
        AvancerDuneLettre2(buttonTextNonCrypted6);
        CrypterUneLettre2(buttonTextCrypted6, buttonTextCrypted6);
    }

    void OnClickButtonNonCrypted7(object sender, EventArgs e)
    {
        AvancerDuneLettre2(buttonTextNonCrypted7);
        CrypterUneLettre2(buttonTextNonCrypted7, buttonTextCrypted7);
    }
}
