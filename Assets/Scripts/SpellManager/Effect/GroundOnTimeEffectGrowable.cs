using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GroundOnTimeEffectGrowable : GroundOnTimeEffect
{
    public GroundOnTimeEffectGrowable(JSONObject js) :base(js)
    {    }

    /// <summary>
    /// Adds a GroundOnTimeAppliedEffect to every Hexagon in the area of effect.
    /// </summary>
    /// <param name="hexagons">The Hexagons in the area of effect.</param>
    /// <param name="target">The Hexagon aimed.</param>
    /// <param name="caster">The caster of the effect.</param>
    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        foreach (Hexagon hexa in hexagons)
        {
            GroundOnTimeAppliedEffectGrowable effect = new GroundOnTimeAppliedEffectGrowable(_id, _effectDirect, _nbTurn, caster);
            hexa.AddOnTimeEffect(effect);
        }
    }
}

