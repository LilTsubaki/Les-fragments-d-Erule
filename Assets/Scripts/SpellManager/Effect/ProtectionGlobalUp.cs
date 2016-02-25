using System;
using System.Collections.Generic;

public class ProtectionGlobalUp : ProtectionGlobalModification
{
    public ProtectionGlobalUp(int id, int protection) : base(id, protection)
    {
    }

    public ProtectionGlobalUp(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _protection = (int)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply protection up effect : " + _protection);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveProtectionGlobalUp(_protection);
        }
    }

}
