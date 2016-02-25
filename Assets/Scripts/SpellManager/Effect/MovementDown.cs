using System;
using System.Collections.Generic;

public class MovementDown : MovementModification
{
    public MovementDown(int id, int movement) : base(id, movement)
    {
    }

    public MovementDown(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _movement = (int)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply movement down effect : " + _movement);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveMovementDown(_movement);
        }
    }

}

