using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PlayerOnTimeEffect : EffectOnTime
{
    public PlayerOnTimeEffect(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _effectDirect = SpellManager.getInstance().getDirectEffectById((uint)js.GetField(js.keys[1]).n);
        _nbTurn = (uint)js.GetField(js.keys[2]).n;
    }

    /// <summary>
    /// Adds a PlayerOnTimeAppliedEffect on every Character in the area of effect.
    /// </summary>
    /// <param name="hexagons">The Hexagons affected by the Effect.</param>
    /// <param name="target">The Hexagon aimed.</param>
    /// <param name="caster">The caster of the effect.</param>
    public new void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach(Character c in chars)
        {
            PlayerOnTimeAppliedEffect effect = new PlayerOnTimeAppliedEffect(_id, _effectDirect, _nbTurn, caster);
            c.ReceiveOnTimeEffect(effect);
        }
    }
}
