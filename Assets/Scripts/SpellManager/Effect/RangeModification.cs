using System;


public class RangeModification : EffectDirect
{
	protected uint _range;

    public RangeModification() : base() { }

	public RangeModification (uint id, uint range): base(id)
	{
		_range = range;
	}
}

