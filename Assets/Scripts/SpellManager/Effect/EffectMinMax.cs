using System;

public class EffectMinMax : EffectDirect
{
	protected uint _min;
    protected uint _max;
    protected Element _element;

    public EffectMinMax() : base() { }

	public EffectMinMax (uint id, uint min, uint max, Element element): base(id)
	{
		_min = min;
		_max = max;
        _element = element;
	}

	public uint GetRandom(){
		return (uint) (new Random ().Next ((int)_min, (int)_max));
	}
}

