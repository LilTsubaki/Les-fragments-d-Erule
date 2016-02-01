using System;

public class DamageElement : EffectMinMax
{

	private Element _element;
	public DamageElement (uint min, uint max, Element element) : base(min, max) 
	{
		_element = element;
	}
}

