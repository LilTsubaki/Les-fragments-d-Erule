using System;
using System.Collections.Generic;

public class ProtectionGlobalDown : ProtectionGlobalModification
{
    public ProtectionGlobalDown(int id, int protection) : base(id, protection)
    {
    }

    public ProtectionGlobalDown(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _protection = (int)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply protection down effect : " + _protection);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveProtectionGlobalDown(_protection);
        }
    }

}
