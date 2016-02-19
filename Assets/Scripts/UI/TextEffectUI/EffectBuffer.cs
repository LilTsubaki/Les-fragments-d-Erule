using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectBuffer {

    private float _step;
    private float _currentTime;

    private Queue<TextEffect> _textEffects;

    public EffectBuffer(float step)
    {
        _step = step;
        _currentTime = 0;
        _textEffects = new Queue<TextEffect>();
    }

    public void UpdateTimer()
    {
        _currentTime += Time.deltaTime;
    }
    public void AddTextEffect(TextEffect textEffect)
    {
        _textEffects.Enqueue(textEffect);
    }

    public TextEffect DequeueTextEffect()
    {
        if (_currentTime >= _step && _textEffects.Count >0)
        {
            _currentTime = 0;
            return _textEffects.Dequeue();
        }

        return null;
    }

}
