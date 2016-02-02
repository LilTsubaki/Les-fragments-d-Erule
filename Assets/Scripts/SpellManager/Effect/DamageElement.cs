using System;
using System.Collections.Generic;

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

    new public void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        //TODO
        //la normalement on a un liste qu'on récupère a partir de la liste des hexagons mais ya personne qui 'la codé donc je suis bézé
        List<Character> chars = new List<Character>();
        foreach (Character c in chars)
        {
            c.ReceiveDamage((uint)new Random().Next((int)_min, (int)_max + 1));
        }
    }
}

