using System;
using System.Collections.Generic;

public class RangeDown: RangeModification
{
	public RangeDown (int id, int range): base(id, range)
	{
	}

    public RangeDown(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _range = (int)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply range down effect : " + _range);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveRangeModifier(-_range);
        }
    }

}

