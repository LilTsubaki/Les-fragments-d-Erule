using System;
using System.Collections.Generic;

public class Spell
{
    private uint id;
    private uint areaId;
    private Effects effectsArea;
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

    public Effects EffectsArea
    {
        get
        {
            return effectsArea;
        }

        set
        {
            effectsArea = value;
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

