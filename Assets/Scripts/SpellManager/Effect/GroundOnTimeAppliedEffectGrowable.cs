using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class GroundOnTimeAppliedEffectGrowable : GroundOnTimeAppliedEffect
{
    public GroundOnTimeAppliedEffectGrowable(int id, Effect effect, int nbTurn, Character caster) : base(id, effect, nbTurn, caster)
    {

    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        if (_nbTurn > 0)
        {
            _effect.ApplyEffect(hexagons, target, _caster);
        }
        else
        {
            Logger.Debug("Tried to apply a GroundOnTimeAppliedEffect with 0 as nbTurn");
        }
    }

    public void GrowUp(Hexagon hexagon)
    {
        Logger.Debug("grow up GroundOnTimeAppliedEffect begin");
        if (PlayBoardManager.GetInstance().isMyTurn(_caster)) { 
            foreach (Hexagon hex in hexagon.GetAllNeighbours())
            {
                Logger.Debug("add on time effect on hexa begin");
                hex.AddOnTimeEffect(new GroundOnTimeAppliedEffectGrowable(_id, _effect, _nbTurn, _caster));
                Logger.Debug("add on time effect on hexa end");
            }
        }
        Logger.Debug("grow up GroundOnTimeAppliedEffect end");
    }
}

