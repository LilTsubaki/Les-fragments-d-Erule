using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


public abstract class EffectTerminable: EffectDirect
{

    private int _nbTurn;
    private bool _applyReverseEffect = false;

    public EffectTerminable()
    {

    }
    public EffectTerminable(int id, int nbTurn, bool applyReverseEffect = true) : base(id)
    {
        _nbTurn = nbTurn;
        _applyReverseEffect = applyReverseEffect;
    }
    
    public int NbTurn
    {
        get
        {
            return _nbTurn;
        }

        set
        {
            _nbTurn = value;
        }
    }

    protected bool ApplyReverseEffect
    {
        get
        {
            return _applyReverseEffect;
        }

        set
        {
            _applyReverseEffect = value;
        }
    }
}
