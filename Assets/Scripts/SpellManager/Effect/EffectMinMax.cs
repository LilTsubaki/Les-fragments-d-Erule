using System;

public abstract class EffectMinMax : EffectDirect
{
	protected int _min;
    protected int _max;
    public Element _element;

    public EffectMinMax() : base() { }

	public EffectMinMax (int id, int min, int max, Element element): base(id)
	{
		_min = min;
		_max = max;
        _element = element;
	}

	public int GetRandom(){
        return EruleRandom.RangeValue(_min, _max);
	}
}

