using System;

public class DamageElement : EffectMinMax
{

	private Element _element;
	public DamageElement (uint id, uint min, uint max, Element element) : base(id, min, max) 
	{
		_element = element;
	}

    public DamageElement(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _min = (uint)js.GetField(js.keys[1]).n;
        _max = (uint)js.GetField(js.keys[2]).n;
        _element = Element.GetElement((int)js.GetField(js.keys[3]).n);
    }
}

