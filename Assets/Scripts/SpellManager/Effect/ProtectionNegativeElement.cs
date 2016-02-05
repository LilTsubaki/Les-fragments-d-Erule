using System;
using System.Collections.Generic;


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


	public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster){
		List<Character> characters = PlayBoardManager.GetInstance ().GetCharacterInArea (hexagons);

		foreach(var ch in characters){
			ch.ReceiveElementNegativeProtection (_protection, _element);
		}
	}
}

