using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GroundOnTimeEffect : EffectOnTime
{
    public GroundOnTimeEffect(JSONObject js) :base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        try
        {
            _effectDirect = (EffectDirect)SpellManager.getInstance().GetDirectEffectById((uint)js.GetField(js.keys[1]).n);
        }
        catch
        {
            Logger.Error("this is not a ontimeeffect");
        }
        _nbTurn = (uint)js.GetField(js.keys[2]).n;
    }

    /// <summary>
    /// Adds a GroundOnTimeAppliedEffect to every Hexagon in the area of effect.
    /// </summary>
    /// <param name="hexagons">The Hexagons in the area of effect.</param>
    /// <param name="target">The Hexagon aimed.</param>
    /// <param name="caster">The caster of the effect.</param>
    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        foreach(Hexagon hexa in hexagons)
        {
            GroundOnTimeAppliedEffect effect = new GroundOnTimeAppliedEffect(_id, _effectDirect, _nbTurn, caster);
            hexa.AddOnTimeEffect(effect);
        }
    }
}

