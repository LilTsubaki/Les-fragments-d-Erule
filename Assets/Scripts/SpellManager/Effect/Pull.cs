using System;
using System.Collections.Generic;

public class Pull : EffectDeplacement
{
	public Pull (int id, int nbDeplacement, int damage) : base (id, nbDeplacement, damage)
	{
	}

    public Pull(JSONObject js) : base()
    {
        _id = (int)js.GetField("id").n;
        _nbDeplacement = (int)js.GetField("nbDeplacement").n;
        _damage = (int)js.GetField("damage").n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        List<Character> characters = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);

        for (int i = 0; i < hexagons.Count; i++)
        {
            if (!characters[i].IsStabilized)
            {
                Hexagon source = caster.Position;
                Direction.EnumDirection direction = Direction.GetDirection(target, source);
                characters[i].TranslateCharacter(direction, _nbDeplacement);
                characters[i]._state = Character.State.Translating;
            }
        }

        /*for (int i = 0; i < hexagons.Count; i++)
        {
            if (hexagons[i]._entity != null && hexagons[i]._entity is Character)
            {
                Character character = (Character)hexagons[i]._entity;
                if (!character.IsStabilized)
                {
                    Hexagon source = caster.Position;
                    Direction.EnumDirection direction = Direction.GetDirection(target, source);
                    character.TranslateCharacter(direction, _nbDeplacement);
                    character._state = Character.State.Translating;
                    Logger.Trace("Hexagon " + i + " has a character");
                }                    
            }
            else
            {
                Logger.Trace("Hexagon " + i + " doesn't have a character");
            }
        }*/
    }
}

