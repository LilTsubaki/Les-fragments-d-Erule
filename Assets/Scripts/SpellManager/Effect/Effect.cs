using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public abstract class Effect
{
    protected int _id;

    public Effect() { }

	public Effect(int id) 
	{
		_id = id;
	}

    public abstract void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster);

    public int GetId()
    {
        return _id;
    }
}

