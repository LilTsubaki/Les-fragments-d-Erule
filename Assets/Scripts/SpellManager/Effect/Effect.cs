using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public abstract class Effect
{
    protected uint _id;

    public Effect() { }

	public Effect(uint id) 
	{
		_id = id;
	}

    public abstract void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster);

    public uint GetId()
    {
        return _id;
    }
}

