using System;
using System.Collections.Generic;

public class Spell
{
    protected uint _id;
    protected uint _areaId;
    protected Effects _effectsArea;

    public Spell (uint id)
	{
        _id = id;
	}

    public Spell (JSONObject js)
    {
    }
}

