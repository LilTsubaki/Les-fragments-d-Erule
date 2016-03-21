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

    public void RegisterEntity(Entity Entity)
    {
        if(Entity != null)
        _buffers.Add(Entity, new EffectBuffer(_step));
    }

    public void DeleteEntity(Entity Entity)
    {
        _buffers.Remove(Entity);
    }

    public void AddTextEffect(Entity Entity, TextEffect textEffect)
    {
        if(_buffers.ContainsKey(Entity))
            _buffers[Entity].AddTextEffect(textEffect);
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
