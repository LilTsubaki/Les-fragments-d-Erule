﻿using System.Collections.Generic;

public class GroundOnTimeAppliedEffect : EffectOnTime {

    protected Character _caster;

    public GroundOnTimeAppliedEffect(uint id, EffectDirect effectDirect, uint nbTurn, Character caster)
    {
        _id = id;
        _effectDirect = effectDirect;
        _nbTurn = nbTurn;
        _caster = caster;
    }

    public new void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        if (_nbTurn > 0)
        {
            _effectDirect.ApplyEffect(hexagons, target, _caster);
        }
        else
        {
            Logger.Debug("Tried to apply a GroundOnTimeAppliedEffect with 0 as nbTurn");
        }
    }

    public Character GetCaster()
    {
        return _caster;
    }

    /// <summary>
    /// Reduces the number of turns remaining. Removes the effect from the Hexagon if nbTurn inferior to 1.
    /// </summary>
    /// <param name="hexagon">The Hexagon affected by the effect.</param>
    public void ReduceNbTurn(Hexagon hexagon)
    {
        _nbTurn--;
        if(_nbTurn < 1)
        {
            hexagon.RemoveOnTimeEffect(this);
        }
    }

}