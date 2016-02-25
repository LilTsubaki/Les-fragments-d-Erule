using System;
using System.Collections.Generic;

public class HealUp : HealModification
{
    public HealUp(int id, int heal) : base(id, heal)
    {
    }

    public HealUp(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _heal = (int)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply heal up effect : " + _heal);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveHealUp(_heal);
        }
    }

}
