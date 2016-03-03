﻿using System.Collections.Generic;

public class PlayerOnTimeAppliedEffect : EffectOnTime {

    protected Character _caster;

	public PlayerOnTimeAppliedEffect(int id, EffectDirect effectDirect, int nbTurn, Character caster)
    {
        _id = id;
        EffectDirect = effectDirect;
        _nbTurn = nbTurn;
        _caster = caster;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        if (_nbTurn-- > 0)
        {
            EffectDirect.ApplyEffect(hexagons, target, _caster);
        }
        else
        {
            foreach(Character c in PlayBoardManager.GetInstance().GetCharacterInArea(hexagons))
            {
                Logger.Trace("Removing effect from " + c.Name);
                c.RemoveOnTimeEffect(this);
            }
        }
    }
    
}
