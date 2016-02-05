using System;
using System.Collections.Generic;

public class ProtectionGlobal : EffectDirect
{
	protected uint _protection;

    public ProtectionGlobal():base() {}
	public ProtectionGlobal (uint id, uint protection): base(id)
	{
		_protection = protection;
	}

    public ProtectionGlobal(JSONObject js) : base()
    {
        _id = (uint)js.GetField(js.keys[0]).n;
        _protection = (uint)js.GetField(js.keys[1]).n;
    }

	public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster){
		List<Character> characters = PlayBoardManager.GetInstance ().GetCharacterInArea (hexagons);

		foreach(var ch in characters){
			ch.ReceiveGlobalProtection (_protection);
		}
	}
}

