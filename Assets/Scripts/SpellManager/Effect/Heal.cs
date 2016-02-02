using System;
using System.Collections.Generic;

public class Heal : EffectMinMax
{
    public Heal(uint id, uint min, uint max) : base(id, min, max)
    {
    }

    public Heal(JSONObject js)
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _min = (uint)js.GetField(js.keys[1]).n;
        _max = (uint)js.GetField(js.keys[2]).n;
    }

    new public void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveHeal((uint)new Random().Next((int)_min, (int)_max + 1));
        }
    }
}
