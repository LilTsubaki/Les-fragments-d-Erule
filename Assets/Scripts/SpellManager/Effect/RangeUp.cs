using System;
using System.Collections.Generic;

public class RangeUp: RangeModification
{
	public RangeUp (uint id, int range): base(id, range)
	{
	}

    public RangeUp(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _range = (int)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveRangeModifier(_range);
        }
    }
}

