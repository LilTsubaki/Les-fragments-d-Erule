using System;


public abstract class RangeModification : EffectDirect
{
	protected int _range;

    public RangeModification() : base() { }

	public RangeModification (uint id, int range): base(id)
	{
		_range = range;
	}

}

