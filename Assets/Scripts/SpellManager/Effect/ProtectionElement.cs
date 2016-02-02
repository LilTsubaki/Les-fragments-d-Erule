using System;


public class ProtectionElement : ProtectionGlobal
{
	Element _element;
	public ProtectionElement (uint protection, Element element):base(protection)
	{
		_element = element;
	}
}

