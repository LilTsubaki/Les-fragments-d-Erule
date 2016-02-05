using System;
using System.Collections.Generic;

public class SelfSpell:Spell
{
    public readonly Effects _effects;
	public readonly Effects _effectsCrit;

    public SelfSpell (uint id) :base(id)
	{
	}

    public SelfSpell(JSONObject js) : base(js)
    {
        Id = (uint)js.GetField(js.keys[0]).n;
        _areaId = (uint)js.GetField("areaId").n;
		_effectsArea = new Effects(js.GetField("effectsAreaIds"));
		_effectsAreaCrit = new Effects(js.GetField("effectsAreaCritIds"));
		_effects = new Effects(js.GetField("effectsIds"));
		_effectsCrit = new Effects(js.GetField("effectsCritIds"));
    }
}

