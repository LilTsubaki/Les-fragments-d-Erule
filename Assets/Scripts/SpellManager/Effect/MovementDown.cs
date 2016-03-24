using System;
using System.Collections.Generic;

public class MovementDown : MovementModification
{
    public MovementDown(int id, int movement, int nbTurn, bool applyReverseEffect = true) : base(id, movement, nbTurn, applyReverseEffect)
    {
    }

    public MovementDown(JSONObject js) : base()
    {
        _id = (int)js.GetField(js.keys[0]).n;
        _movement = (int)js.GetField(js.keys[1]).n;
        NbTurn = (int)js.GetField("nbTurn").n;
        ApplyReverseEffect = true;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Trace("Apply movement down effect : " + _movement);
        List<Character> chars = PlayBoardManager.GetInstance().GetCharacterInArea(hexagons);
        foreach (Character c in chars)
        {
            if (!c.EffectsTerminable.ContainsKey(_id) || !ApplyReverseEffect)
            {
                c.ReceiveMovementDown(_movement);
            }
            if (ApplyReverseEffect)
            {
                c.EffectsTerminable[_id] = new MovementUp(_id, _movement, NbTurn, false);
            }
        }
    }

}

