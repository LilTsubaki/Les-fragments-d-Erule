using System;
using System.Collections.Generic;

public class Spell
{
    private int id;
    private int areaId;
    private Effects effectsArea;
    protected Effects _effectsAreaCrit;

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

    public Spell (int id)
	{
        Id = id;
	}

    public Spell (JSONObject js)
    {
    }
}

