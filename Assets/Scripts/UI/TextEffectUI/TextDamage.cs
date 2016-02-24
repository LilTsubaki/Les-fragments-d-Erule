﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

class TextDamage : TextEffect
{
    private int _value;
    private Element _element;

    public TextDamage(int value, Element element)
    {
        _value = value;
        _element = element;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Character character)
    {
        textEffect.GameObject.transform.position = character._gameObject.transform.position + new Vector3(0, 1, 0);
        textEffect.GameObject.GetComponent<TextEffectBehaviour>().InitialPosition = textEffect.GameObject.transform.position;

        Image image = textEffect.GameObject.GetComponentInChildren<Image>();
        Text text = textEffect.GameObject.GetComponentInChildren<Text>();

        text.text = _value.ToString();
        text.color = ColorErule._damage;

        textEffect.GameObject.GetComponent<TextEffectBehaviour>()._hasAnImage = true;

        image.sprite = _element.getSprite();

    }
}
