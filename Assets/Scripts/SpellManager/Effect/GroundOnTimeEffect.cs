using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GroundOnTimeEffect : EffectOnTime
{
    public GroundOnTimeEffect(JSONObject js) :base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _effectDirect = SpellManager.getInstance().getDirectEffectById((uint)js.GetField(js.keys[1]).n);
        _nbTurn = (uint)js.GetField(js.keys[2]).n;
    }
}

