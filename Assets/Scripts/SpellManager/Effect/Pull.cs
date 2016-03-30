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

        for (int i = 0; i < characters.Count; i++)
        {
            if (!characters[i].IsStabilized)
            {
                Hexagon source = caster.Position;
                Direction.EnumDirection direction = Direction.GetDirection(target, source);
                characters[i].TranslateCharacter(direction, _nbDeplacement);
                characters[i].CharacterState = Character.State.Translating;
            }
        }
    }
}

