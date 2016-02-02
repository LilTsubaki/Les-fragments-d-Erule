using System;

public class Pull : EffectDeplacement
{
	public Pull (uint id, uint nbDeplacement) : base (id, nbDeplacement)
	{
	}

    public Pull(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _nbDeplacement = (uint)js.GetField(js.keys[1]).n;
    }
}

