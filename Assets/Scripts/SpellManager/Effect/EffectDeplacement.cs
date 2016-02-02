using System;


public class EffectDeplacement : EffectDirect
{
	protected uint _nbDeplacement;
	protected Hexagon _source;
	public EffectDeplacement (uint id, uint nbDeplacement, Hexagon source): base(id)
	{
		_nbDeplacement = nbDeplacement;
		_source = source;
	}
}
