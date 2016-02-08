using System;
using System.Collections.Generic;

public class Spell
{
    private uint id;
    private uint areaId;
    protected Effects _effectsArea;
	protected Effects _effectsAreaCrit;

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

    public uint AreaId
    {
        get
        {
            return areaId;
        }

        set
        {
            areaId = value;
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

