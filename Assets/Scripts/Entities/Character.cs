using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    public GameObject _gameObject;
    
    public enum State { Moving, CastingSpell, Waiting, Translating }

	public static int MaxProtection = 50;
	public readonly int _lifeMax;
	public int _lifeCurrent;

    public static int _maxActionPoints;
    private int _currentActionPoints;

    private string _name;

    private int _turnNumber;

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

    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }

    public int RangeModifier
    {
        get
        {
            return _rangeModifier;
        }

        set
        {
            _rangeModifier = value;
        }
    }

    public int TurnNumber
    {
        get
        {
            return _turnNumber;
        }

        set
        {
            _turnNumber = value;
        }
    }

    public int CurrentActionPoints
    {
        get
        {
            return _currentActionPoints;
        }

        set
        {
            _currentActionPoints = value;
        }
    }

    public State _state;

	private Dictionary<Element, int> _protections;
	private Dictionary<Element, int> _protectionsNegative;

	private int _globalProtection;
	private int _globalNegativeProtection;

	private int _sommeProtection;
	private int _sommeNegativeProtection;

    private Dictionary<int, PlayerOnTimeAppliedEffect> _onTimeEffects;
    private List<int> _onTimeEffectsToRemove;

    private int _rangeModifier;

    public Character (int lifeMax, Hexagon position, GameObject go) : base(position)
	{
        _gameObject = GameObject.Instantiate(go);
        _gameObject.transform.position = position.GameObject.transform.position + new Vector3(0, 0.5f, 0);
        _gameObject.GetComponent<CharacterBehaviour>()._character = this;

		_protections = new Dictionary<Element, int> ();
		_protectionsNegative = new Dictionary<Element, int> ();

		_globalProtection = 0;
		_globalNegativeProtection = 0;

		_sommeProtection = 0;
		_sommeNegativeProtection = 0;

		_lifeMax = lifeMax;
		_lifeCurrent = lifeMax;

        _rangeModifier = 0;

        _maxActionPoints = 4;
        _currentActionPoints = 1;
        _turnNumber = 0;

		foreach (var e in Element.GetElements()) {
			_protections [e] = 0;
			_protectionsNegative [e] = 0;
		}

        _onTimeEffects = new Dictionary<int, PlayerOnTimeAppliedEffect>();
        _onTimeEffectsToRemove = new List<int>();
        _state = State.Waiting;
	}

    public void ReceiveHeal(int value)
    {
        if (_lifeCurrent + value > _lifeMax)
            _lifeCurrent = _lifeMax;
        else
            _lifeCurrent += value;
    }

    public void ReceiveDamage(int value, Element element)
    {
        int positiveElementResistance;
        int negativeElementResistance;

        _protections.TryGetValue(element, out positiveElementResistance);
        _protectionsNegative.TryGetValue(element, out negativeElementResistance);

        int finalValue = (positiveElementResistance - negativeElementResistance) + (_globalProtection - _globalNegativeProtection);
        float percentage = (100 - finalValue) / 100.0f;
        value = (int)(value * percentage);


        if (_lifeCurrent - value < 0)
            _lifeCurrent = 0;
        else
            _lifeCurrent -= value;
        
    }

	public void ReceiveGlobalProtection(int protection){
		
		int val = Math.Min (protection, _globalNegativeProtection);

		_globalNegativeProtection -= val;
		_sommeNegativeProtection -= val;

		protection -= val;

		int max = MaxProtection - _sommeProtection;

		_globalProtection += Math.Min (max, protection);
		_sommeProtection += Math.Min (max, protection);
	}

	public void ReceiveGlobalNegativeProtection(int protection){

		int val = Math.Min (protection, _globalProtection);

		_globalProtection -= val;
		_sommeProtection -= val;

		protection -= val;

		int max = MaxProtection - _sommeNegativeProtection;

		_globalNegativeProtection += Math.Min (max, protection);
		_sommeNegativeProtection += Math.Min (max, protection);
	}

	public void ReceiveElementProtection(int protection, Element element){
		
		int val = Math.Min (protection, _protectionsNegative[element]);

		_protectionsNegative[element] -= val;
		_sommeNegativeProtection -= val;

		protection -= val;

		int max = MaxProtection - _sommeProtection;

		_protections[element] += Math.Min (max, protection);
		_sommeProtection += Math.Min (max, protection);
        Logger.Trace("nouvelle valeur : " + _sommeProtection);
	}

	public void ReceiveElementNegativeProtection(int protection, Element element){
		
		int val = Math.Min (protection, _protections[element]);

		_protections[element] -= val;
		_sommeProtection -= val;

		protection -= val;

		int max = MaxProtection - _sommeNegativeProtection;

		_protectionsNegative[element] += Math.Min (max, protection);
		_sommeNegativeProtection += Math.Min (max, protection);
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
        _onTimeEffectsToRemove.Add(effect.GetId());
        Logger.Trace("Removed");
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
            Logger.Trace("Applying OnTimeEffect " + effect.GetId());
            effect.ApplyEffect(hexagons, _position, this);
        }
    }


    public void RemoveMarkedOnTimeEffects()
    {
        foreach (int id in _onTimeEffectsToRemove)
        {
            _onTimeEffects.Remove(id);
        }
    }

    public void ReceiveRangeModifier(int value)
    {
        RangeModifier += value;
    }

    public int GetElementResistance(Element elem)
    {
        int res = 0;
        _protections.TryGetValue(elem, out res);
        return res;
    }

    public int GetElementWeakness(Element elem)
    {
        int res = 0;
        _protectionsNegative.TryGetValue(elem, out res);
        return res;
    }

    public int GetGlobalResistance()
    {
        return _globalProtection;
    }

    public int GetGlobalWeakness()
    {
        return _globalNegativeProtection;
    }

    /// <summary>
    /// Translate the character given a direction, it stops if the hexagon in the direction is not reachable
    /// </summary>
    /// <param name="direction">Direction where the character is translated</param>
    /// <param name="count">Number of hexagon the character is translated</param>
    /// <returns></returns>
    public Hexagon TranslateCharacter(Direction.EnumDirection direction, int count)
    {
        Logger.Debug("Count : " + count + ", Hexagon : " + _position._posX + ", " + _position._posY);

        if (count == 0)
            return _position;

        Hexagon target = _position.GetHexa(direction);
        if (!target.isReachable())
            return _position;
        else
        {
            _position._entity = null;
            target._entity = this;
            _position = target;

            return TranslateCharacter(direction, count - 1);
        }
    }
}
