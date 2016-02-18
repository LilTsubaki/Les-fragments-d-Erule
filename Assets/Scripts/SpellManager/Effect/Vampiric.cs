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
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            int damage = (int)new Random().Next((int)_min, (int)_max + 1);
            int heal = _vampiricPercentage * damage / 100;
            c.ReceiveDamage(damage, _element);
            caster.ReceiveHeal(heal);
        }
    }
}
