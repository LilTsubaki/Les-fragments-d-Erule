using System;
using System.Collections.Generic;

public class Push : EffectDeplacement
{
	public Push (uint id, uint nbDeplacement, uint damage) : base (id, nbDeplacement, damage)
	{
	}

    public Push(JSONObject js) : base()
    {
        _id = (uint)js.GetField("id").n;
        _nbDeplacement = (uint)js.GetField("nbDeplacement").n;
        _damage = (uint)js.GetField("damage").n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        for (int i = 0; i < hexagons.Count; i++)
        {
            if (hexagons[i]._entity != null && hexagons[i]._entity is Character)
            {
                Character character = (Character)hexagons[i]._entity;
                Hexagon source = caster.Position;
                Direction.EnumDirection direction = Direction.GetDirection(source, target);
                character.TranslateCharacter(direction, _nbDeplacement);
                character._state = Character.State.Translating;
                Logger.Trace("Hexagon " + i + " has a character");
            }
            else
            {
                Logger.Trace("Hexagon " + i + " doesn't have a character");
            }
        }
    }
}

