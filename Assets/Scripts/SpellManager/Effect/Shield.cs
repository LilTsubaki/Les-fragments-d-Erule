using System.Collections.Generic;

public class Shield : EffectDirect
{
    private int _shieldValue;

    

    public Shield(int id, int shield) : base(id)
    {
        ShieldValue = shield;
    }

    public Shield(JSONObject js) 
    {
        _id = (int)js.GetField("id").n;
        ShieldValue = (int)js.GetField("value").n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        List<Killable> killables = PlayBoardManager.GetInstance().GetKillableInArea(hexagons);
        foreach (Character k in killables)
        {
            k.ReceiveShield(this);
        }
    }

    public int ShieldValue
    {
        get
        {
            return _shieldValue;
        }

        set
        {
            _shieldValue = value;
        }
    }
}
