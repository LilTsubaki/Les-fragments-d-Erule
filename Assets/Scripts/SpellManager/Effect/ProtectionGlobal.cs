using System;

public class ProtectionGlobal : EffectDirect
{
	protected uint _protection;

    public ProtectionGlobal():base() {}
	public ProtectionGlobal (uint id, uint protection): base(id)
	{
		_protection = protection;
	}

    public ProtectionGlobal(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _protection = (uint)js.GetField(js.keys[1]).n;
    }
}

