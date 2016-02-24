﻿using UnityEngine;
using System.Collections.Generic;

public class SpellAnimationManager {

    static private SpellAnimationManager _instance;
    private Dictionary<string, SpellAnimation> _animations;

    private SpellAnimationManager()
    {
        _animations = new Dictionary<string, SpellAnimation>();
    }

    public static SpellAnimationManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new SpellAnimationManager();
        }

        return _instance;
    }

    public bool Register(string id, SpellAnimation anim)
    {
        if (_animations.ContainsKey(id))
            return false;

        _animations.Add(id, anim);
        return true;
    }

    public bool Play(string id, GameObject from, GameObject to)
    {
        SpellAnimation anim;
        if(_animations.TryGetValue(id, out anim))
        {
            anim.Reset(from, to);
            anim.Play();
            return true;
        }
        return false;
    }
}