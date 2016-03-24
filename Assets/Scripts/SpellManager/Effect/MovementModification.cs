using System;


public abstract class MovementModification : EffectTerminable
{
    protected int _movement;

    public MovementModification() : base() { }

    public MovementModification(int id, int movement, int nbTurn, bool applyReverseEffect = true) : base(id, nbTurn, applyReverseEffect)
    {
        _movement = movement;
    }

}

