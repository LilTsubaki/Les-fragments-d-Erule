using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TextRangeLoss : TextEffect
{
    private int _value;

    public TextRangeLoss(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Entity entity)
    {
        SetupTextEffect(textEffect, entity, "- " + _value.ToString() + " PORTÉE", ColorErule._loss);
    }
}
