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
	uint _globalProtection;
	uint _globalNegativeProtection;

	public Character (uint lifeMax, Hexagon position)
	{
		_protections = new Dictionary<Element, uint> ();
		_protectionsNegative = new Dictionary<Element, uint> ();
		_lifeMax = lifeMax;
		_lifeCurrent = lifeMax;
		_position = position;

		foreach (var e in Element.GetElements()) {
			_protections [e] = 0;
			_protectionsNegative [e] = 0;
		}
	}

    public void ReceiveHeal(uint value)
    {
        if (_lifeCurrent + value > _lifeMax)
            _lifeCurrent = _lifeMax;
        else
            _lifeCurrent += value;
    }

    public void ReceiveDamage(uint value, Element element)
    {
        uint positiveElementResistance;
        uint negativeElementResistance;

        _protections.TryGetValue(element, out positiveElementResistance);
        _protectionsNegative.TryGetValue(element, out negativeElementResistance);

        uint finalValue = (positiveElementResistance - negativeElementResistance) + (_globalProtection - _globalNegativeProtection);
        float percentage = (100 - finalValue) / 100;
        value = (uint)(value * percentage);

        if (_lifeCurrent - value < 0)
            _lifeCurrent = 0;
        else
            _lifeCurrent -= value;
        
    }
}
