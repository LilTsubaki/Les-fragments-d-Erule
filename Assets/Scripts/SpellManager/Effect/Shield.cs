using System.Collections.Generic;

public class Shield : EffectDirect
{
    private int _shieldValue;
    private int _numberTurn;
    
    public Shield(Shield shield)
    {
        _id = shield._id;
        _shieldValue = shield.ShieldValue;
        _numberTurn = shield.NumberTurn;
    }

    public Shield(int id, int shield, int numberTurn) : base(id)
    {
        ShieldValue = shield;
        NumberTurn = numberTurn;
    }

    public Shield(JSONObject js) 
    {
        _id = (int)js.GetField("id").n;
        ShieldValue = (int)js.GetField("value").n;
        NumberTurn = (int)js.GetField("value").n;
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

    public int NumberTurn
    {
        get
        {
            return _numberTurn;
        }

        set
        {
            _numberTurn = value;
        }
    }
}
