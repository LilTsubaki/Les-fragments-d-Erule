using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextResistanceGain : TextEffect
{
    private int _value;
    private Element _element;

    public TextResistanceGain(int value, Element element)
    {
        _value = value;
        _element = element;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Character character)
    {
        SetupTextEffect(textEffect, character, "+ " + _value.ToString() + "% RÉSISTANCE", ColorErule._gain);
    }
}
