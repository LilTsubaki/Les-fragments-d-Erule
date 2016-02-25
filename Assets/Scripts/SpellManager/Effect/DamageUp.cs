using System;
using System.Collections.Generic;

public class DamageUp : DamageModification
{
    public DamageUp(int id, int damage) : base(id, damage)
    {
    }

    public DamageUp(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _damage = (int)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply damage up effect : " + _damage);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveDamageUp(_damage);
        }
    }

}
