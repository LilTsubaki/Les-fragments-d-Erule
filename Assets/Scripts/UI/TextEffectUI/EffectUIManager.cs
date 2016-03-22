using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectUIManager {

    private static EffectUIManager manager;
    private Dictionary<Entity, EffectBuffer> _buffers;
    private Pool<TextEffectPoolable> _pool;

    private float _step;

    private EffectUIManager()
    {
        _buffers = new Dictionary<Entity, EffectBuffer>();
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
        _step = 1.4298f;
    }

    public bool Contains(Entity entity)
    {
        return _buffers.ContainsKey(entity);
    }
    public void RegisterEntity(Entity entity)
    {
        if (entity != null)
            _buffers.Add(entity, new EffectBuffer(entity, _step));
    }

    public void DeleteEntity(Entity entity)
    {
        if (_buffers.ContainsKey(entity))
        {
            if (_buffers[entity].TextEffects.Count == 0)
            {
                _buffers.Remove(entity);
            }
            else
            {
                _buffers[entity].Delete = true;
            }
        }
       
    }

    public void AddTextEffect(Entity entity, TextEffect textEffect)
    {
        if(_buffers.ContainsKey(entity))
            _buffers[entity].AddTextEffect(textEffect);
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
