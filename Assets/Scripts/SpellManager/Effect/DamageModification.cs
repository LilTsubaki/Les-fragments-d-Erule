using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class DamageModification : EffectTerminable
{
    protected int _damage;

    public DamageModification() : base() { }

    public DamageModification(int id, int damage, int nbTurn,bool applyReverseEffect = true) : base(id, nbTurn, applyReverseEffect)
    {
        _damage = damage;
    }

}