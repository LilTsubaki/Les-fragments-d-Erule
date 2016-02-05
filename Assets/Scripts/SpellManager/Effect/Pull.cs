using System;
using System.Collections.Generic;

public class Pull : EffectDeplacement
{
	public Pull (uint id, uint nbDeplacement) : base (id, nbDeplacement)
	{
	}

    public Pull(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _nbDeplacement = (uint)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        throw new NotImplementedException();
    }
}

