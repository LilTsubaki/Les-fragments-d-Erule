using System;

public abstract class EffectOnTime : Effect
{
    public Effect _effect;
    public int _nbTurn;

    public EffectOnTime() : base() { }

	public EffectOnTime (int id, Effect effectDirect, int nbTurn): base(id)
	{
        _effect = effectDirect;
		_nbTurn = nbTurn;
	}
}

