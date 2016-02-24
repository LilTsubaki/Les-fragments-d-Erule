using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextRangeGain : TextEffect {

    private int _value;

    public TextRangeGain(int value)
    {
        _value = value;
    }

    public override void DisplayText(TextEffectPoolable textEffect, Character character)
    {
        textEffect.GameObject.transform.position = character._gameObject.transform.position + new Vector3(0, 1, 0);
        textEffect.GameObject.GetComponent<TextEffectBehaviour>().InitialPosition = textEffect.GameObject.transform.position;

        Image image = textEffect.GameObject.GetComponentInChildren<Image>();
        Text text = textEffect.GameObject.GetComponentInChildren<Text>();

        text.text = "+ "+_value.ToString()+" PORTÉE";
        text.color = ColorErule._gain;

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        textEffect.GameObject.GetComponent<TextEffectBehaviour>()._hasAnImage = false;
    }
}
