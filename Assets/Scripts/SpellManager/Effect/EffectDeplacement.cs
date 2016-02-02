using System;


public class EffectDeplacement : EffectDirect
{
	protected uint _nbDeplacement;

    public EffectDeplacement() : base() { }

	public EffectDeplacement (uint id, uint nbDeplacement): base(id)
	{
		_nbDeplacement = nbDeplacement;
	}
}
