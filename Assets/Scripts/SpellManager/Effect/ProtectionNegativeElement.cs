using System;


public class ProtectionNegativeElement : ProtectionNegativeGlobal
{
	Element _element;
	public ProtectionNegativeElement (uint protection, Element element):base(protection)
	{
		_element = element;
	}
}

