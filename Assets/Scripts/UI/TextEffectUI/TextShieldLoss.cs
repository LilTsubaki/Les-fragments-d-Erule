using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TextShieldLoss : TextEffect
{
    private int _value;

    public TextShieldLoss(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Entity entity)
    {
        SetupTextEffect(textEffect, entity, "- " + _value.ToString() + " BOUCLIER", ColorErule._loss);
    }
}

