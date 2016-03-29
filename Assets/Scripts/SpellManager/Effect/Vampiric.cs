using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Vampiric : EffectMinMax
{
    private int _vampiricPercentage;

    public Vampiric(int id, int min, int max, Element element, int vampiricPercentage) : base(id, min, max, element)
    {
        _vampiricPercentage = vampiricPercentage;
    }

    public Vampiric(JSONObject js)
    {
        _id = (int)js.GetField("id").n;
        _min = (int)js.GetField("minValue").n;
        _max = (int)js.GetField("maxValue").n;
        _element = Element.GetElement((int)js.GetField("element").n);
        _vampiricPercentage = (int)js.GetField("vampiricPercentage").n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        int damage = GetRandom();
        List<Killable> killables = PlayBoardManager.GetInstance().GetKillableInArea(hexagons);

        foreach (var k in killables)
        {
            damage = k.ReceiveDamage(damage, _element, caster);
            int heal = _vampiricPercentage * damage / 100;
            if (k is Character)
                caster.ReceiveHeal(heal);
        }
    }
}
