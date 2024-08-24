using System;
using TMPro;
using UnityEngine;

public class ScreenText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;


    public void ChangeColor(string hexColor)
    {
        const char hexSharp = '#';

        if (!hexColor.StartsWith(hexSharp))
            hexColor = hexSharp + hexColor;
        
        UnityEngine.Color col;
        if (!ColorUtility.TryParseHtmlString(hexColor, out col))
            throw new Exception();

        _text.color = col;
    }

    public void ChangeText(string text)
    {
        _text.text = text;
    }
}