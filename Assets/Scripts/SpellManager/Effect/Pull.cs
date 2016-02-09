using System;
using System.Collections.Generic;

public class Pull : EffectDeplacement
{
	public Pull (uint id, uint nbDeplacement, uint damage) : base (id, nbDeplacement, damage)
	{
	}

    public Pull(JSONObject js) : base()
    {
        _id = (uint)js.GetField("id").n;
        _nbDeplacement = (uint)js.GetField("nbDeplacement").n;
        _damage = (uint)js.GetField("damage").n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        if (target._entity != null && target._entity is Character)
        {
            Character character = (Character)target._entity;
            Hexagon source = caster.Position;
            Direction.EnumDirection direction = Direction.GetDirection(target, source);
            character.TranslateCharacter(direction, _nbDeplacement);
            character._state = Character.State.Translating;
        }
        else
        {
            Logger.Trace("Hexagon doesn't have a character");
        }
    }
}

