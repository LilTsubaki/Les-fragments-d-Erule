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
        
		_id = (uint)js.GetField("id").n;
		_areaId = (uint)js.GetField("areaId").n;
		_effectsArea = new Effects(js.GetField("effectsAreaIds"));
		_effectsAreaCrit = new Effects(js.GetField("effectsAreaCritIds"));
		_rangeId = (uint)js.GetField("rangeId").n;
    }

}

