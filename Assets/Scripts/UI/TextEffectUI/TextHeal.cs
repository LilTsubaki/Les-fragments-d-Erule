using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

class TextHeal : TextEffect
{
    private int _value;

    public TextHeal(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Character character)
    {
        Image image = textEffect.GameObject.GetComponentInChildren<Image>();
        Text text = textEffect.GameObject.GetComponentInChildren<Text>();

        text.text = _value.ToString();
        text.color = Color.green;

        image.color = new Color(0, 0, 0, 0);
    }
}
