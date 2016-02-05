using System;
using System.Collections.Generic;

public class Spell
{
    private uint id;
    protected uint _areaId;
    protected Effects _effectsArea;

    public uint Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public Spell (uint id)
	{
        Id = id;
	}

    public Spell (JSONObject js)
    {
    }
}

