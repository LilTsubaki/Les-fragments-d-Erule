using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

class TextHeal : TextEffect
{
    private int _value;

    public TextHeal(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Entity entity)
    {
        SetupTextEffect(textEffect, entity, _value.ToString(), ColorErule._heal);        
    }
}
