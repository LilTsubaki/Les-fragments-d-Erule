using System;
using System.Collections.Generic;

public class TargetSpell:Spell
{
    public readonly int _rangeId;

	public TargetSpell(int id) : base(id)
    {
	}

    public TargetSpell(JSONObject js) : base(js)
    {
        Id = (int)js.GetField(js.keys[0]).n;
		AreaId = (int)js.GetField("areaId").n;
		EffectsArea = new Effects(js.GetField("effectsAreaIds"));
		EffectsAreaCrit = new Effects(js.GetField("effectsAreaCritIds"));
		_rangeId = (int)js.GetField("rangeId").n;
    }

}

