using System;


public class ProtectionNegativeElement : ProtectionNegativeGlobal
{
	Element _element;
	public ProtectionNegativeElement (uint id, uint protection, Element element):base(id, protection)
	{
		_element = element;
	}
}

