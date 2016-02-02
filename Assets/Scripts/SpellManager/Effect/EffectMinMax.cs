using System;

public class EffectMinMax : EffectDirect
{
	private uint _min;
	private uint _max;

	public EffectMinMax (uint id, uint min, uint max): base(id)
	{
		_min = min;
		_max = max;
	}

	public uint GetRandom(){
		return (uint) (new Random ().Next ((int)_min, (int)_max));
	}
}

