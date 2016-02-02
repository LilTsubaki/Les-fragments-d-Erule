using System;

public class EffectOnTime : Effect
{
	private EffectDirect _effectDirect;
	private uint _nbTurn;

	public EffectOnTime (uint id, EffectDirect effectDirect, uint nbTurn): base(id)
	{
		_effectDirect = effectDirect;
		_nbTurn = nbTurn;
	}
}

