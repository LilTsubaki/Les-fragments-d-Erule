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

    public override void DisplayText(TextEffectPoolable textEffect, Character character)
    {
        textEffect.GameObject.transform.position = character._gameObject.transform.position + new Vector3(0, 1, 0);
        textEffect.GameObject.GetComponent<TextEffectBehaviour>().InitialPosition = textEffect.GameObject.transform.position;

        Image image = textEffect.GameObject.GetComponentInChildren<Image>();
        Text text = textEffect.GameObject.GetComponentInChildren<Text>();

        text.text = "- " + _value.ToString() + " MOUVEMENT";
        text.color = ColorErule._loss;

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        textEffect.GameObject.GetComponent<TextEffectBehaviour>()._hasAnImage = false;
    }
}
