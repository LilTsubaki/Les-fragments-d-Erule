using System;

public abstract class EffectOnTime : Effect
{
	protected EffectDirect _effectDirect;
    protected uint _nbTurn;

    public EffectOnTime() : base() { }

	public EffectOnTime (uint id, EffectDirect effectDirect, uint nbTurn): base(id)
	{
		_effectDirect = effectDirect;
		_nbTurn = nbTurn;
	}
}

