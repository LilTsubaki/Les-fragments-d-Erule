using System;
using System.Collections.Generic;

public class DamageDown : DamageModification
{
    public DamageDown(int id, int damage) : base(id, damage)
    {
    }

    public DamageDown(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _damage = (int)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply damage down effect : " + _damage);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveDamageDown(_damage);
        }
    }

}
