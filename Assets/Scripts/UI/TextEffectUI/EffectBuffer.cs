using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectBuffer {

    private float _step;
    private float _currentTime;

    private Queue<TextEffect> _textEffects;
    private bool _delete = false;

    private Entity _entity;

    private bool _paused = true;

    public Queue<TextEffect> TextEffects
    {
        get
        {
            return _textEffects;
        }

        set
        {
            _textEffects = value;
        }
    }

    public bool Delete
    {
        get
        {
            return _delete;
        }

        set
        {
            _delete = value;
        }
    }

    public bool Paused
    {
        get
        {
            return _paused;
        }

        set
        {
            _paused = value;

        }
    }

    public float CurrentTime
    {
        get
        {
            return _currentTime;
        }

        set
        {
            _currentTime = value;
        }
    }

    public EffectBuffer(Entity entity, float step)
    {
        _step = step;
        _currentTime = 0;
        TextEffects = new Queue<TextEffect>();
        _entity = entity;
    }

    public void UpdateTimer()
    {
        _currentTime += Time.deltaTime;
    }
    public void AddTextEffect(TextEffect textEffect)
    {
        TextEffects.Enqueue(textEffect);
    }

    public TextEffect DequeueTextEffect()
    {
        if (TextEffects.Count == 0 )
        {
            Paused = true;
            return null;
        }

        if (_currentTime >= _step)
        {
            _currentTime = 0;

            if (!Paused)
            {
                TextEffect te = TextEffects.Dequeue();
               
                if (Delete && TextEffects.Count == 0)
                {
                    EffectUIManager.GetInstance().DeleteEntity(_entity);
                }
                return te;
            }

        }

        return null;
    }

}
