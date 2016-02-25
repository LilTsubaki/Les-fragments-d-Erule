using System;
using System.Collections.Generic;

public class MovementUp : MovementModification
{
    public MovementUp(int id, int movement) : base(id, movement)
    {
    }

    public MovementUp(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _movement = (int)js.GetField(js.keys[1]).n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply movement up effect : " + _movement);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            c.ReceiveMovementUp(_movement);
        }
    }

}

