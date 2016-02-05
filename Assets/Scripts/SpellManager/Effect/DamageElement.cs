using System;
using System.Collections.Generic;

public class DamageElement : EffectMinMax
{
	public DamageElement (uint id, uint min, uint max, Element element) : base(id, min, max, element) 
	{
	}

    public DamageElement(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _min = (uint)js.GetField(js.keys[1]).n;
        _max = (uint)js.GetField(js.keys[2]).n;
        _element = Element.GetElement((int)js.GetField(js.keys[3]).n);
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply elem damage effect");
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveDamage((uint)new Random().Next((int)_min, (int)_max + 1), _element);
        }
    }
}

