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
        _go = GameObject.Instantiate(go);
        _go.transform.SetParent(parent.transform, false);
        _go.SetActive(false);
    }

    public GameObject GameObject
    {
        get
        {
            return _go;
        }

        set
        {
            _go = value;
        }
    }

    public void Copy(TextEffectPoolable t)
    {
        _go = GameObject.Instantiate(t._go);
        _go.transform.SetParent(t._go.transform.parent, false);
        _go.SetActive(false);
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
