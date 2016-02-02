using System;

public class RangeDown: RangeModification
{
	public RangeDown (uint id, uint range): base(id, range)
	{
	}

    public RangeDown(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _range = (uint)js.GetField(js.keys[1]).n;
    }

}

