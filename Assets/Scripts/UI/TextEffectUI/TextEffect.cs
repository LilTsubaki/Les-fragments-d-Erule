using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class TextEffect {

    protected void SetupTextEffect(TextEffectPoolable textEffect, Entity entity, string text, Color color, Sprite sprite = null)
    {
        //textEffect.GameObject.transform.position = entity.GameObject.transform.position + new Vector3(0, 1, 0);
        textEffect.GameObject.GetComponent<TextEffectBehaviour>().InitialPosition = textEffect.GameObject.transform.position;

        Text textComponent = textEffect.GameObject.GetComponentInChildren<Text>();
        textComponent.text = text;
        textComponent.color = color;
        Image image = textEffect.GameObject.GetComponentInChildren<Image>();

        if (sprite != null)
        {
            textEffect.GameObject.GetComponent<TextEffectBehaviour>()._hasAnImage = true;
            image.sprite = sprite;
        }
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            textEffect.GameObject.GetComponent<TextEffectBehaviour>()._hasAnImage = false;
        }

    }

    public abstract void DisplayText(TextEffectPoolable textEffect, Entity entity);
}
