using System;

public class EffectMinMax
{
	private uint _min;
	private uint _max;

	public EffectMinMax (uint min, uint max)
	{
		_min = min;
		_max = max;
	}

	public uint GetRandom(){
		return (uint) (new Random ().Next ((int)_min, (int)_max));
	}
}

