using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

class TextDamage : TextEffect
{
    private int _value;
    private Element _element;

    public TextDamage(int value, Element element)
    {
        _value = value;
        _element = element;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Character character)
    {
        SetupTextEffect(textEffect, character, _value.ToString(), ColorErule._damage, _element.getSprite());
    }
}
