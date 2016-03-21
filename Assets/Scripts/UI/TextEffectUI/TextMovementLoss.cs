using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextMovementLoss : TextEffect
{

    private int _value;

    public TextMovementLoss(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Entity entity)
    {
        SetupTextEffect(textEffect, entity, "- " + _value.ToString() + " MOUVEMENT", ColorErule._loss);
    }
}
