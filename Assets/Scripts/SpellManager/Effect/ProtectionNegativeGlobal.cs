using System;

public class ProtectionNegativeGlobal : EffectDirect
{
	protected uint _protection;

    public ProtectionNegativeGlobal() : base() { }

	public ProtectionNegativeGlobal (uint id, uint protection): base(id)
	{
		_protection = protection;
	}
    public ProtectionNegativeGlobal(JSONObject js) :base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _protection = (uint)js.GetField(js.keys[1]).n;
    }
}


