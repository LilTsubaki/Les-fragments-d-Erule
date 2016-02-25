using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectUIManager {

    private static EffectUIManager manager;
    private Dictionary<Character, EffectBuffer> _buffers;
    private Pool<TextEffectPoolable> _pool;

    private EffectUIManager()
    {
        _buffers = new Dictionary<Character, EffectBuffer>();
    }

    public static EffectUIManager GetInstance()
    {
        if (manager == null)
            manager = new EffectUIManager();

        return manager;
    }

    public void Init(GameObject go, GameObject parent)
    {
        _pool = new Pool<TextEffectPoolable>(new TextEffectPoolable(go, parent),50,5);
    }

    public void RegisterCharacter(Character character, float step)
    {
        if(character != null)
        _buffers.Add(character, new EffectBuffer(step));
    }

    public void DeleteCharacter(Character character)
    {
        _buffers.Remove(character);
    }

    public void AddTextEffect(Character character, TextEffect textEffect)
    {
        if(_buffers.ContainsKey(character))
            _buffers[character].AddTextEffect(textEffect);
    }

    public void UpdateTimer()
    {
        foreach(var buffer in _buffers)
        {
            buffer.Value.UpdateTimer();
        }
    }

    public void Update()
    {
        UpdateTimer();

        foreach(var buffer in _buffers)
        {
            TextEffect tEffect = buffer.Value.DequeueTextEffect();
            if(tEffect != null)
            {
                tEffect.DisplayText(_pool.GetPoolable(), buffer.Key);
            }
        }
    }

}
