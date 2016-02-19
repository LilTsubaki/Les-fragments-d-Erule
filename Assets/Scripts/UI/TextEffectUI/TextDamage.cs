using UnityEngine;
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
        throw new NotImplementedException();
    }
}
