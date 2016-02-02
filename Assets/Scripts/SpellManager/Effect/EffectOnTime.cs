using System;

public class EffectOnTime : Effect
{
	private EffectDirect _effectDirect;
	private uint _nbTurn;

	public EffectOnTime (uint id, EffectDirect effectDirect, uint nbTurn): base(id)
	{
		_effectDirect = effectDirect;
		_nbTurn = nbTurn;
	}

    public EffectOnTime(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _effectDirect =  SpellManager.getInstance().getDirectEffectById((uint)js.GetField(js.keys[1]).n);
        _nbTurn = (uint)js.GetField(js.keys[2]).n;
    }
}

