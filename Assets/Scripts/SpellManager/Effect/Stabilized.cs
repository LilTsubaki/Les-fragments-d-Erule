using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Stabilized : EffectDirect
{
    public Stabilized(int id) : base(id)
    {
    }

    public Stabilized(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply stabilization");
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveStabilization();
        }
    }
}

