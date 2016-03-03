using System;

public abstract class EffectOnTime : Effect
{
    private EffectDirect effectDirect;
    protected int _nbTurn;

    public EffectDirect EffectDirect
    {
        get
        {
            return effectDirect;
        }

        set
        {
            effectDirect = value;
        }
    }

    public EffectOnTime() : base() { }

	public EffectOnTime (int id, EffectDirect effectDirect, int nbTurn): base(id)
	{
		EffectDirect = effectDirect;
		_nbTurn = nbTurn;
	}
}

