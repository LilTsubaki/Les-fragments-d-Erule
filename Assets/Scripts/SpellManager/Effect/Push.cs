using System;

public class Push : EffectDeplacement
{
	public Push (uint id, uint nbDeplacement) : base (id, nbDeplacement)
	{
	}

    public Push(JSONObject js) :base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _nbDeplacement = (uint)js.GetField(js.keys[1]).n;
    }
}

