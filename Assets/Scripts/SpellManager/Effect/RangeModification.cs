using System;


public abstract class RangeModification : EffectTerminable
{
	protected int _range;

    public RangeModification() : base() { }

	public RangeModification (int id, int range, int nbTurn, bool applyReverseEffect = true) : base(id, nbTurn, applyReverseEffect)
    {
		_range = range;
	}

}

