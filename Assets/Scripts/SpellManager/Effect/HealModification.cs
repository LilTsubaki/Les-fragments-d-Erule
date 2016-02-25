using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class HealModification : EffectDirect
{
    protected int _heal;

    public HealModification() : base() { }

    public HealModification(int id, int heal) : base(id)
    {
        _heal = heal;
    }

}