using System;

public class DamageElement : EffectMinMax
{

	private Element _element;
	public DamageElement (uint id, uint min, uint max, Element element) : base(id, min, max) 
	{
		_element = element;
	}
}

