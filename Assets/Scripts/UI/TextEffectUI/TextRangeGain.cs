using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextRangeGain : TextEffect {

    private int _value;

    public TextRangeGain(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Entity entity)
    {
        SetupTextEffect(textEffect, entity, "+ " + _value.ToString() + " PORTÉE", ColorErule._gain);
    }
}
