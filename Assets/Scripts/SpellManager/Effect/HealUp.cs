using System;
using System.Collections.Generic;

public class HealUp : HealModification
{
    public HealUp(int id, int heal, int nbTurn, bool applyReverseEffect = true) : base(id, heal, nbTurn, applyReverseEffect)
    {
    }

    public HealUp(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _heal = (int)js.GetField(js.keys[1]).n;
        NbTurn = (int)js.GetField("nbTurn").n;
        ApplyReverseEffect = true;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply heal up effect : " + _heal);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            if (!c.EffectsTerminable.ContainsKey(_id) || !ApplyReverseEffect)
            {
                c.ReceiveHealUp(_heal);
            }
            if (ApplyReverseEffect)
            {
                c.EffectsTerminable[_id] = new HealDown(_id, _heal, NbTurn, false);
            }
        }
    }

}
