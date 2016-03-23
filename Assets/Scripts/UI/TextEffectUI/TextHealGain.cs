using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TextHealGain : TextEffect
{

    private int _value;

    public TextHealGain(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Entity entity)
    {
        SetupTextEffect(textEffect, entity, "+ " + _value.ToString() + " AUX SOINS", ColorErule._gain);
    }
}


