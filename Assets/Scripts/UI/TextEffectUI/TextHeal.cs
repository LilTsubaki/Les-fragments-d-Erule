using UnityEngine;
using System.Collections;
using System;

class TextHeal : TextEffect
{
    private int _value;

    public TextHeal(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Character character)
    {
        throw new NotImplementedException();
    }
}
