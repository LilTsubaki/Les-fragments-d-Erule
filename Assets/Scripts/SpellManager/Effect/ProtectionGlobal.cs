using System;
using System.Collections.Generic;

public class ProtectionGlobal : EffectDirect
{
	protected int _protection;

    public ProtectionGlobal():base() {}
	public ProtectionGlobal (int id, int protection): base(id)
	{
		_protection = protection;
	}

    public ProtectionGlobal(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _protection = (int)js.GetField(js.keys[1]).n;
    }

	public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply protection effect : " + _protection);
        List<Character> characters = PlayBoardManager.GetInstance ().GetCharacterInArea (hexagons);

		foreach(var ch in characters){
			ch.ReceiveGlobalProtection (_protection);
		}
	}
}

