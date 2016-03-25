using System;
using System.Collections.Generic;

public class RangeUp: RangeModification
{
	public RangeUp(int id, int range, int nbTurn, bool applyReverseEffect = true) : base(id, range, nbTurn, applyReverseEffect)
    {
	}

    public RangeUp(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _range = (int)js.GetField(js.keys[1]).n;
        NbTurn = (int)js.GetField("nbTurn").n;
        ApplyReverseEffect = true;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply range up effect : " + _range);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            if (!c.EffectsTerminable.ContainsKey(_id) || !ApplyReverseEffect)
            {
                c.ReceiveRangeModifier(_range);
            }
            if (ApplyReverseEffect)
            {
                c.EffectsTerminable[_id] = new RangeDown(_id, _range, NbTurn, false);

                HistoricManager.GetInstance().AddText(String.Format(StringsErule.rangeBonus, c.Name, _range, NbTurn));
            }
        }
    }
}

