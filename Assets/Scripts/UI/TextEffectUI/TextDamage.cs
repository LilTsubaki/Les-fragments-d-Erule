using UnityEngine;
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
        text.color = Color.red;

        textEffect.GameObject.GetComponent<TextEffectBehaviour>()._hasAnImage = true;

        switch (_element._name)
        {
            case "Fire":
                image.sprite = Resources.Load<Sprite>("Sprites/Feu");
                break;
            case "Water":
                image.sprite = Resources.Load<Sprite>("Sprites/Eau");
                break;
            case "Air":
                image.sprite = Resources.Load<Sprite>("Sprites/Air");
                break;
            case "Earth":
                image.sprite = Resources.Load<Sprite>("Sprites/Terre");
                break;
            case "Wood":
                image.sprite = Resources.Load<Sprite>("Sprites/Bois");
                break;
            case "Metal":
                image.sprite = Resources.Load<Sprite>("Sprites/Metal");
                break;
            default:
                break;
        }
    }
}
