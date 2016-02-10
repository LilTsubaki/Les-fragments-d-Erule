﻿using System;
using System.Collections.Generic;


public class ProtectionElement : ProtectionGlobal
{
	Element _element;
	public ProtectionElement (int id, int protection, Element element):base(id, protection)
	{
		_element = element;
	}

    public ProtectionElement(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _protection = (int)js.GetField(js.keys[1]).n;
        _element = Element.GetElement((int)js.GetField(js.keys[2]).n);
    }

	public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply protection element effect : " + _protection + " element : " + _element);
        List<Character> characters = PlayBoardManager.GetInstance ().GetCharacterInArea (hexagons);

		foreach(var ch in characters){
			ch.ReceiveElementProtection (_protection, _element);
		}
	}
}

