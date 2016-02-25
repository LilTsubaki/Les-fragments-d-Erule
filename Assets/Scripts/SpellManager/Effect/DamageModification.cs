using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class DamageModification : EffectDirect
{
    protected int _damage;

    public DamageModification() : base() { }

    public DamageModification(int id, int damage) : base(id)
    {
        _damage = damage;
    }

}