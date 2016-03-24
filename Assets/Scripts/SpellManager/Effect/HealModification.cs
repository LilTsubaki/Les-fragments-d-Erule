using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class HealModification : EffectTerminable
{
    protected int _heal;

    public HealModification() : base() { }

    public HealModification(int id, int heal, int nbTurn, bool applyReverseEffect = true) : base(id, nbTurn, applyReverseEffect)
    {
        _heal = heal;
    }

}