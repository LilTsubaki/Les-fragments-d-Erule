﻿using System;
using System.Collections.Generic;

public class ProtectionNegativeGlobal : EffectDirect
{
	protected int _protection;

    public ProtectionNegativeGlobal() : base() { }

	public ProtectionNegativeGlobal (int id, int protection): base(id)
	{
		_protection = protection;
	}

    public ProtectionNegativeGlobal(JSONObject js) :base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _protection = (int)js.GetField(js.keys[1]).n;
    }

	public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply negative protection element effect : " + _protection);
        List<Character> characters = PlayBoardManager.GetInstance ().GetCharacterInArea (hexagons);

		foreach(var ch in characters){
			ch.ReceiveGlobalNegativeProtection (_protection);
		}
	}
}


