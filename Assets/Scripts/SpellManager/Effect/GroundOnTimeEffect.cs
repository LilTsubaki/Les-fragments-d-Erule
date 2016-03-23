using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class GroundOnTimeEffect : EffectOnTime
{
    public GroundOnTimeEffect(JSONObject js) :base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        try
        {
            _effect = SpellManager.getInstance().GetDirectEffectById((int)js.GetField(js.keys[1]).n);
        }
        catch
        {
            Logger.Error("this is not a ontimeeffect (" + _id + ")");
            Logger.Error("Direct id : " + (int)js.GetField(js.keys[1]).n);
        }
        _nbTurn = (int)js.GetField(js.keys[2]).n;
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
            GroundOnTimeAppliedEffect effect = new GroundOnTimeAppliedEffect(_id, _effect, _nbTurn, caster);
            hexa.AddOnTimeEffect(effect);
        }
        HistoricManager.GetInstance().AddText(String.Format(StringsErule.area, caster.Name));
    }
}

