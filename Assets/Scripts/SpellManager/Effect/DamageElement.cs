using System;
using System.Collections.Generic;

public class DamageElement : EffectMinMax
{
	public DamageElement (int id, int min, int max, Element element) : base(id, min, max, element) 
	{
	}

    public DamageElement(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _min = (int)js.GetField(js.keys[1]).n;
        _max = (int)js.GetField(js.keys[2]).n;
        _element = Element.GetElement((int)js.GetField(js.keys[3]).n);
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            int damage = (int)new Random().Next((int)_min, (int)_max + 1);

            damage += caster.DamageModifier;
            c.ReceiveDamage(damage, _element);
        }
    }
}

