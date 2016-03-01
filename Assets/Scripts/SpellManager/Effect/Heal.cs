using System;
using System.Collections.Generic;

public class Heal : EffectMinMax
{
    public Heal(int id, int min, int max, Element element) : base(id, min, max, element)
    {
    }

    public Heal(JSONObject js)
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _min = (int)js.GetField(js.keys[1]).n;
        _max = (int)js.GetField(js.keys[2]).n;
        //déccommenter pour les heals elems
        //_element = Element.GetElement((int)js.GetField(js.keys[3]).n);
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            int heal = GetRandom();
            Logger.Trace("Apply heal effect : " + heal);
            c.ReceiveHeal(heal);
        }
    }
}
