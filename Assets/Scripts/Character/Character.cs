using System;
using System.Collections.Generic;

public class Character
{
	public readonly uint _lifeMax;
	public uint _lifeCurrent;
	Hexagon _position;
	public uint _currentActionPoints;
	Dictionary<Element, uint> _protections;
	Dictionary<Element, uint> _protectionsNegative;

	public Character (uint lifeMax, Hexagon position)
	{
		_lifeMax = lifeMax;
		_lifeCurrent = lifeMax;
		_position = position;

		foreach (var e in Element.GetElements()) {
			_protections [e] = 0;
			_protectionsNegative [e] = 0;
		}
	}
}
