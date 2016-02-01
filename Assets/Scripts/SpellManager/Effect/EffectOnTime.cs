using System;

public class EffectOnTime : Effect
{
	private EffectDirect _effectDirect;
	private uint _nbTurn;

	public EffectOnTime (EffectDirect effectDirect, uint nbTurn)
	{
		_effectDirect = effectDirect;
		_nbTurn = nbTurn;
	}
}

