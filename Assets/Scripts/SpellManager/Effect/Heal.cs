using System;

public class Heal : EffectMinMax
{
    public Heal(uint id, uint min, uint max) : base(id, min, max)
    {
    }

    public Heal(JSONObject js)
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _min = (uint)js.GetField(js.keys[1]).n;
        _max = (uint)js.GetField(js.keys[2]).n;
    }
}

