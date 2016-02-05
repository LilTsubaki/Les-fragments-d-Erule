using System;
using System.Collections.Generic;

public class SelfSpell:Spell
{
    public readonly Effects _effects;

    public SelfSpell (uint id) :base(id)
	{
	}

    public SelfSpell(JSONObject js) : base(js)
    {
        Id = (uint)js.GetField(js.keys[0]).n;
        _areaId = (uint)js.GetField(js.keys[1]).n;
        _effectsArea = new Effects(js.GetField(js.keys[2]));
        _effects = new Effects(js.GetField(js.keys[3]));
    }
}

