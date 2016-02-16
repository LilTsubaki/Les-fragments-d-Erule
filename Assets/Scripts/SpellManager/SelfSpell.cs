using System;
using System.Collections.Generic;

public class SelfSpell:Spell
{
    public readonly Effects _effects;
	public readonly Effects _effectsCrit;

    public SelfSpell (int id) :base(id)
	{
	}

    public SelfSpell(JSONObject js) : base(js)
    {
        Id = (int)js.GetField(js.keys[0]).n;
        AreaId = (int)js.GetField("areaId").n;
		EffectsArea = new Effects(js.GetField("effectsAreaIds"));
		EffectsAreaCrit = new Effects(js.GetField("effectsAreaCritIds"));
		_effects = new Effects(js.GetField("effectsIds"));
		_effectsCrit = new Effects(js.GetField("effectsCritIds"));
    }
}

