using System;

public class RangeUp: RangeModification
{
	public RangeUp (uint id, uint range): base(id, range)
	{
	}

    public RangeUp(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _range = (uint)js.GetField(js.keys[1]).n;
    }
}

