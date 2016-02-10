using System;

public abstract class EffectOnTime : Effect
{
	protected EffectDirect _effectDirect;
    protected int _nbTurn;

    public EffectOnTime() : base() { }

	public EffectOnTime (int id, EffectDirect effectDirect, int nbTurn): base(id)
	{
		_effectDirect = effectDirect;
		_nbTurn = nbTurn;
	}
}

