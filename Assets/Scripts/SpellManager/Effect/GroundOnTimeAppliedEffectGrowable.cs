using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class GroundOnTimeAppliedEffectGrowable : GroundOnTimeAppliedEffect
{
    public GroundOnTimeAppliedEffectGrowable(int id, EffectDirect effectDirect, int nbTurn, Character caster) : base(id, effectDirect, nbTurn, caster)
    {

    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        if (_nbTurn > 0)
        {
            EffectDirect.ApplyEffect(hexagons, target, _caster);
        }
        else
        {
            Logger.Debug("Tried to apply a GroundOnTimeAppliedEffect with 0 as nbTurn");
        }
    }

    public void GrowUp(Hexagon hexagon)
    {
        if (PlayBoardManager.GetInstance().isMyTurn(_caster)) { 
            foreach (Hexagon hex in hexagon.GetAllNeighbours())
            {
                hex.AddOnTimeEffect(new GroundOnTimeAppliedEffectGrowable(_id, EffectDirect, _nbTurn, _caster));
            }
        }
    }
}

