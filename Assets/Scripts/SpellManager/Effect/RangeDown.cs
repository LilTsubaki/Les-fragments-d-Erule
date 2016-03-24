﻿using System;
using System.Collections.Generic;

public class RangeDown: RangeModification
{
	public RangeDown (int id, int range, int nbTurn, bool applyReverseEffect = true) : base(id,range, nbTurn, applyReverseEffect)
    {
	}

    public RangeDown(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _range = (int)js.GetField(js.keys[1]).n;
        NbTurn = (int)js.GetField("nbTurn").n;
        ApplyReverseEffect = true;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply range down effect : " + _range);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            if (!c.EffectsTerminable.ContainsKey(_id) || !ApplyReverseEffect)
            {
                c.ReceiveRangeModifier(-_range);
            }
            if (ApplyReverseEffect)
            {
                c.EffectsTerminable[_id] = new RangeUp(_id, _range, NbTurn, false);
            }
        }
    }

}

