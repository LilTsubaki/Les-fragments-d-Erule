using UnityEngine;
using System.Collections;
using System;

public class TextEffectPoolable : Poolable<TextEffectPoolable>
{
    private GameObject _go;

    public TextEffectPoolable()
    {

    }

    public TextEffectPoolable(GameObject go, GameObject parent)
    {
        _go = UnityEngine.Object.Instantiate(go);
        _go.transform.parent = parent.transform;
    }

    public void Copy(TextEffectPoolable t)
    {
        _go = GameObject.Instantiate(t._go);
        _go.transform.parent = t._go.transform;
    }

    public bool IsReady()
    {
        return !_go.activeSelf;
    }

    public void Pick()
    {
        _go.SetActive(true);
    }
}
