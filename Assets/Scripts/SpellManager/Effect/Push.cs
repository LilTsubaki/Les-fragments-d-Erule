using System;
using System.Collections.Generic;

public class Push : EffectDeplacement
{
	public Push (uint id, uint nbDeplacement, uint damage) : base (id, nbDeplacement, damage)
	{
	}

    public Push(JSONObject js) :base()
    {
        _id = (uint)js.GetField("id").n;
        _nbDeplacement = (uint)js.GetField("nbDeplacement").n;
        _damage = (uint)js.GetField("damage").n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        

    }
}

