using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class ProtectionGlobalModification : EffectDirect
{
    protected int _protection;

    public ProtectionGlobalModification() : base() { }

    public ProtectionGlobalModification(int id, int protection) : base(id)
    {
        _protection = protection;
    }

}