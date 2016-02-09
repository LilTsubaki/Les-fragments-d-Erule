using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    public GameObject _gameObject;

    public enum State { Moving, CastingSpell, Waiting }

	public static uint MaxProtection = 50;
	public readonly uint _lifeMax;
	public uint _lifeCurrent;
	public uint _currentActionPoints;

    private List<Hexagon> _pathToFollow;

    public List<Hexagon> PathToFollow
    {
        get
        {
            return _pathToFollow;
        }

        set
        {
            _pathToFollow = value;
        }
    }

    private int _currentStep;
    public int CurrentStep
    {
        get
        {
            return _currentStep;
        }

        set
        {
            _currentStep = value;
        }
    }

    public State _state;

	private Dictionary<Element, uint> _protections;
	private Dictionary<Element, uint> _protectionsNegative;

	private uint _globalProtection;
	private uint _globalNegativeProtection;

	private uint _sommeProtection;
	private uint _sommeNegativeProtection;

    private Dictionary<uint, PlayerOnTimeAppliedEffect> _onTimeEffects;

    private int _rangeModifier;

    

    public Character (uint lifeMax, Hexagon position, GameObject go) : base(position)
	{
        _gameObject = GameObject.Instantiate(go);
        _gameObject.transform.position = position.GameObject.transform.position + new Vector3(0, 0.5f, 0);
        _gameObject.GetComponent<CharacterBehaviour>()._character = this;

		_protections = new Dictionary<Element, uint> ();
		_protectionsNegative = new Dictionary<Element, uint> ();

		_globalProtection = 0;
		_globalNegativeProtection = 0;

		_sommeProtection = 0;
		_sommeNegativeProtection = 0;

		_lifeMax = lifeMax;
		_lifeCurrent = lifeMax;

        _rangeModifier = 0;

		foreach (var e in Element.GetElements()) {
			_protections [e] = 0;
			_protectionsNegative [e] = 0;
		}

        _onTimeEffects = new Dictionary<uint, PlayerOnTimeAppliedEffect>();
        _state = State.Waiting;
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

	public void ReceiveGlobalProtection(uint protection){
		
		uint val = Math.Min (protection, _globalNegativeProtection);

		_globalNegativeProtection -= val;
		_sommeNegativeProtection -= val;

		protection -= val;

		uint max = MaxProtection - _sommeProtection;

		_globalProtection += Math.Min (max, protection);
		_sommeProtection += Math.Min (max, protection);
	}

	public void ReceiveGlobalNegativeProtection(uint protection){

		uint val = Math.Min (protection, _globalProtection);

		_globalProtection -= val;
		_sommeProtection -= val;

		protection -= val;

		uint max = MaxProtection - _sommeNegativeProtection;

		_globalNegativeProtection += Math.Min (max, protection);
		_sommeNegativeProtection += Math.Min (max, protection);
	}

	public void ReceiveElementProtection(uint protection, Element element){
		
		uint val = Math.Min (protection, _protectionsNegative[element]);

		_protectionsNegative[element] -= val;
		_sommeNegativeProtection -= val;

		protection -= val;

		uint max = MaxProtection - _sommeProtection;

		_protections[element] += Math.Min (max, protection);
		_sommeProtection += Math.Min (max, protection);
        Logger.Trace("nouvelle valeur : " + _sommeProtection);
	}

	public void ReceiveElementNegativeProtection(uint protection, Element element){
		
		uint val = Math.Min (protection, _protections[element]);

		_protections[element] -= val;
		_sommeProtection -= val;

		protection -= val;

		uint max = MaxProtection - _sommeNegativeProtection;

		_protectionsNegative[element] += Math.Min (max, protection);
		_sommeProtection += Math.Min (max, protection);
	}

    /// <summary>
    /// Adds a PlayerOnTimeAppliedEffect to the Character.
    /// </summary>
    /// <param name="effect">The effect to affect the Character by.</param>
    public void ReceiveOnTimeEffect(PlayerOnTimeAppliedEffect effect)
    {
        _onTimeEffects[effect.GetId()] = effect;
    }

    /// <summary>
    /// Removes a PlayerOnTimeAppliedEffect from the Character. Used when no longer active.
    /// </summary>
    /// <param name="effect">The effect to remove.</param>
    public void RemoveOnTimeEffect(PlayerOnTimeAppliedEffect effect)
    {
        Logger.Trace("Removing effect " + effect.GetId() + ".");
        _onTimeEffects.Remove(effect.GetId());
    }

    /// <summary>
    /// Applies every effect the Character is affected by.
    /// </summary>
    public void ApplyOnTimeEffects()
    {
        List<Hexagon> hexagons = new List<Hexagon>();
        hexagons.Add(_position);
        foreach (PlayerOnTimeAppliedEffect effect in _onTimeEffects.Values)
        {
            effect.ApplyEffect(hexagons, _position, this);
        }
    }

    public void ReceiveRangeModifier(int value)
    {
        _rangeModifier += value;
    }

    public uint GetElementResistance(Element elem)
    {
        uint res = 0;
        _protections.TryGetValue(elem, out res);
        return res;
    }

    public uint GetElementWeakness(Element elem)
    {
        uint res = 0;
        _protectionsNegative.TryGetValue(elem, out res);
        return res;
    }

    public uint GetGlobalResistance()
    {
        return _globalProtection;
    }

    public uint GetGlobalWeakness()
    {
        return _globalNegativeProtection;
    }

    /// <summary>
    /// Translate the character given a direction, it stops if the hexagon in the direction is not reachable
    /// </summary>
    /// <param name="direction">Direction where the character is translated</param>
    /// <param name="count">Number of hexagon the character is translated</param>
    /// <returns></returns>
    public Hexagon TranslateCharacter(Direction.EnumDirection direction, uint count)
    {
        if (count == 0)
            return _position;

        Hexagon target = _position.GetHexa(direction);
        if (!target.isReachable())
            return _position;
        else
        {
            _position = target;
            return TranslateCharacter(direction, count - 1);
        }
    }
}
