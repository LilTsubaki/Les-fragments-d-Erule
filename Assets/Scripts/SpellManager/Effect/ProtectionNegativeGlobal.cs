using System;

public class ProtectionNegativeGlobal : EffectDirect
{
	protected uint _protection;
	public ProtectionNegativeGlobal (uint id, uint protection): base(id)
	{
		_protection = protection;
	}
}


