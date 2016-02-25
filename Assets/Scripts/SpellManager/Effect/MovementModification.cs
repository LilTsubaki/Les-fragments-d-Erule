using System;


public abstract class MovementModification : EffectDirect
{
    protected int _movement;

    public MovementModification() : base() { }

    public MovementModification(int id, int movement) : base(id)
    {
        _movement = movement;
    }

}

