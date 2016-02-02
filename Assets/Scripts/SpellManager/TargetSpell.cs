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
        _effectsAreaIds = new List<uint>();
        _id = (uint)js.GetField(js.keys[0]).n;
        _areaId = (uint)js.GetField(js.keys[1]).n;

        JSONObject array = js.GetField(js.keys[2]);
        foreach (JSONObject effectsAreaId in array.list)
        {
            _effectsAreaIds.Add((uint)effectsAreaId.n);
        }

        _rangeId = (uint)js.GetField(js.keys[3]).n;
    }

}

