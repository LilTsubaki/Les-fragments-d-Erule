using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextResistanceLoss : TextEffect
{
    private int _value;
    private Element _element;

    public TextResistanceLoss(int value, Element element)
    {
        _value = value;
        _element = element;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Entity entity)
    {
        SetupTextEffect(textEffect, entity, "- " + _value.ToString() + "% RÉSISTANCE", ColorErule._loss, _element.getSprite());
    }
}
