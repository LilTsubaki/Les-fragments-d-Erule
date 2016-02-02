using System;

public class EffectMinMax : EffectDirect
{
	protected uint _min;
    protected uint _max;

    public EffectMinMax() : base() { }

	public EffectMinMax (uint id, uint min, uint max): base(id)
	{
		_min = min;
		_max = max;
	}

	public uint GetRandom(){
		return (uint) (new Random ().Next ((int)_min, (int)_max));
	}
}

