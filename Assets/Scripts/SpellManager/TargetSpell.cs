using System;
using System.Collections.Generic;

public class TargetSpell:Spell
{
    public readonly uint _rangeId;

	public TargetSpell(uint id) : base(id)
    {
	}

    public TargetSpell(JSONObject js) : base(js)
    {
        Id = (uint)js.GetField(js.keys[0]).n;
		AreaId = (uint)js.GetField("areaId").n;
		_effectsArea = new Effects(js.GetField("effectsAreaIds"));
		_effectsAreaCrit = new Effects(js.GetField("effectsAreaCritIds"));
		_rangeId = (uint)js.GetField("rangeId").n;
    }

}

