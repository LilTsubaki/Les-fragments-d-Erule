using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class Effect
{
    protected uint _id;

    public Effect() { }

	public Effect(uint id) 
	{
		_id = id;
	}

	public void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster){
		throw new NotImplementedException ();
	}

    public uint getId()
    {
        return _id;
    }
}

