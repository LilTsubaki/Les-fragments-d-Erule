using System;


public class ProtectionElement : ProtectionGlobal
{
	Element _element;
	public ProtectionElement (uint id, uint protection, Element element):base(id, protection)
	{
		_element = element;
	}
}

