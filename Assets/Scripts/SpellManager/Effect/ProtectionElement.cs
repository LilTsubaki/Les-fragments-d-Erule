using System;
using System.Collections.Generic;


public class ProtectionElement : ProtectionGlobal
{
	Element _element;
	public ProtectionElement (int id, int protection, Element element, int nbTurn, bool applyReverseEffect = true) : base(id, protection, nbTurn, applyReverseEffect)
    {
		_element = element;
	}

    public ProtectionElement(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _protection = (int)js.GetField(js.keys[1]).n;
        _element = Element.GetElement((int)js.GetField(js.keys[2]).n);
        NbTurn = (int)js.GetField("nbTurn").n;
        ApplyReverseEffect = true;
    }

	public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply protection element effect : " + _protection + " element : " + _element);
        List<Character> characters = PlayBoardManager.GetInstance ().GetCharacterInArea (hexagons);

		foreach(var ch in characters)
        {
            if (!ch.EffectsTerminable.ContainsKey(_id) || !ApplyReverseEffect)
            {
                ch.ReceiveElementProtection(_protection, _element);
            }
            if (ApplyReverseEffect)
            {
                ch.EffectsTerminable[_id] = new ProtectionNegativeElement(_id, _protection, _element, NbTurn, false);
            }            
		}
	}
}

