using System;
using System.Collections.Generic;

public class Spell
{
    private int id;
    private int areaId;
    private Effects effectsArea;
    private Effects effectsAreaCrit;

    public int Id
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

    public int AreaId
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

    public Effects EffectsAreaCrit
    {
        get
        {
            return effectsAreaCrit;
        }

        set
        {
            effectsAreaCrit = value;
        }
    }

    public Spell (int id)
	{
        Id = id;
	}

    public Spell (JSONObject js)
    {
    }
}

