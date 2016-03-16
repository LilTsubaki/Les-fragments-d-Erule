using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextMovementGain : TextEffect
{

    private int _value;

    public TextMovementGain(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Character character)
    {
        SetupTextEffect(textEffect, character, "+ " + _value.ToString() + " MOUVEMENT", ColorErule._gain);
    }
}
