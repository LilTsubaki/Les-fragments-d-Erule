using System;
using System.Collections.Generic;

public class SelfSpell:Spell
{
    public readonly List<uint> _effectsIds;

    public SelfSpell (uint id) :base(id)
	{
	}

    public SelfSpell(JSONObject js) : base(js)
    {
        _effectsIds = new List<uint>();
        _effectsAreaIds = new List<uint>();
        _id = (uint)js.GetField(js.keys[0]).n;
        _areaId = (uint)js.GetField(js.keys[1]).n;

        JSONObject array = js.GetField(js.keys[2]);
        foreach (JSONObject effectsAreaId in array.list)
        {
            _effectsAreaIds.Add((uint)effectsAreaId.n);
        }

        array = js.GetField(js.keys[3]);
        foreach (JSONObject effectsId in array.list)
        {
            _effectsIds.Add((uint)effectsId.n);
        }
    }
}

