using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TextShieldGain : TextEffect
{
    private int _value;

    public TextShieldGain(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Character character)
    {
        SetupTextEffect(textEffect, character, "+ " + _value.ToString() + " BOUCLIER", ColorErule._gain);
    }
}
