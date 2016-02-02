using System;


public class EffectDeplacement
{
	protected uint _nbDeplacement;
	protected Hexagon _source;
	public EffectDeplacement (uint nbDeplacement, Hexagon source)
	{
		_nbDeplacement = nbDeplacement;
		_source = source;
	}
}
