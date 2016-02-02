using System;

public class ProtectionGlobal : EffectDirect
{
	protected uint _protection;
	public ProtectionGlobal (uint id, uint protection): base(id)
	{
		_protection = protection;
	}
}

