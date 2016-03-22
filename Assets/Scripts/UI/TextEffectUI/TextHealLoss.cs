using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TextHealLoss : TextEffect
{

    private int _value;

    public TextHealLoss(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Entity entity)
    {
        SetupTextEffect(textEffect, entity, "- " + _value.ToString() + " AUX SOINS", ColorErule._loss);
    }
}


