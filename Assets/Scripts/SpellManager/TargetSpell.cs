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
        
        _id = (uint)js.GetField(js.keys[0]).n;
        _areaId = (uint)js.GetField(js.keys[1]).n;
        _effectsArea = new Effects(js.GetField(js.keys[2]));
        _rangeId = (uint)js.GetField(js.keys[3]).n;
    }

}

