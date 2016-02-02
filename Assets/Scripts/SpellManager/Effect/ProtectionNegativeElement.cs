using System;


public class ProtectionNegativeElement : ProtectionNegativeGlobal
{
	Element _element;
	public ProtectionNegativeElement (uint id, uint protection, Element element):base(id, protection)
	{
		_element = element;
	}

    public ProtectionNegativeElement(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _protection = (uint)js.GetField(js.keys[1]).n;
        _element = Element.GetElement((int)js.GetField(js.keys[2]).n);
    }
}

