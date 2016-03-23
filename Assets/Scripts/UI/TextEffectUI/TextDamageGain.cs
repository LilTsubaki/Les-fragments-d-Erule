using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TextDamageGain : TextEffect
{

    private int _value;

    public TextDamageGain(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Entity entity)
    {
        SetupTextEffect(textEffect, entity, "+ " + _value.ToString() + " DEGATS FIXES", ColorErule._gain);
    }
}

