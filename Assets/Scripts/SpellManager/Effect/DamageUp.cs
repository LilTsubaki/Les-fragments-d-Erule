using System;
using System.Collections.Generic;

public class DamageUp : DamageModification
{
    public DamageUp(int id, int damage, int nbTurn, bool applyReverseEffect = true) : base(id, damage, nbTurn, applyReverseEffect)
    {
    }

    public DamageUp(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _damage = (int)js.GetField(js.keys[1]).n;
        NbTurn = (int)js.GetField("nbTurn").n;
        ApplyReverseEffect = true;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply damage up effect : " + _damage);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            if (!c.EffectsTerminable.ContainsKey(_id) || !ApplyReverseEffect)
            {
                c.ReceiveDamageUp(_damage);
            }
            if (ApplyReverseEffect)
            {
                c.EffectsTerminable[_id] = new DamageDown(_id, _damage, NbTurn, false);
            }
        }
    }

}
