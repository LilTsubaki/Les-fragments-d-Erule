using System;
using System.Collections.Generic;

public class RangeUp: RangeModification
{
	public RangeUp (int id, int range): base(id, range)
	{
	}

    public RangeUp(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _range = (int)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply range up effect : " + _range);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveRangeModifier(_range);
        }
    }
}

