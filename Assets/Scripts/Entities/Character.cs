using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity, Killable
{
    public enum State { Moving, CastingSpell, Waiting, Translating, Dead, Victorious }

	/*public static int MaxProtection = 50;*/
	public int _lifeMax;
	public int _lifeCurrent;

    public static int _maxActionPoints = 3;
    public static int _maxMovementPoints = 3;
    private int _currentActionPoints;
    private int _currentMovementPoints;

    private string _name;

    private int _turnNumber;

    private List<Hexagon> _pathToFollow;

    private State _state;
    private State _nextState;

    private Dictionary<Element, int> _protections;
	private Dictionary<Element, int> _protectionsNegative;

	/*private int _globalProtection;
	private int _globalNegativeProtection;*/

	private int _sumProtection;
	private int _sumNegativeProtection;

    private Dictionary<int, PlayerOnTimeAppliedEffect> _onTimeEffects;
    private Dictionary<int, EffectTerminable> _effectsTerminable;
    private List<int> _onTimeEffectsToRemove;

    private int _rangeModifier;
    private int _damageModifier;
    private int _healModifier;
    private int _globalProtectionModifier;
    private bool _isStabilized;

    private Vector3 _positionOffset = new Vector3(0.0f, 0.0f, 0.0f);
    private LinkedList<Shield> _shields;
    private int _globalShieldValue;

    private bool _getDot = false;
    private bool _getHot = false;

    private List<int> _idAreaAppliedThisTurn;

    public Character(int lifeMax)
    {
        _lifeMax = lifeMax;
        Protections = new Dictionary<Element, int>();
        ProtectionsNegative = new Dictionary<Element, int>();
    }

    public Character(int lifeMax, GameObject go)
    {
        _gameObject = GameObject.Instantiate(go);
        _gameObject.GetComponent<CharacterBehaviour>()._character = this;
        _gameObject.SetActive(false);

        Protections = new Dictionary<Element, int>();
        ProtectionsNegative = new Dictionary<Element, int>();

        /*GlobalProtection = 0;
        GlobalNegativeProtection = 0;*/

        SommeProtection = 0;
        SommeNegativeProtection = 0;

        _lifeMax = lifeMax;
        _lifeCurrent = lifeMax;

        _rangeModifier = 0;
        DamageModifier = 0;
        HealModifier = 0;
        _globalShieldValue = 0;
        GlobalProtectionModifier = 0;

        _currentActionPoints = 1;
        _currentMovementPoints = 1;
        _turnNumber = 1;
        IsStabilized = false;

        foreach (var e in Element.GetElements())
        {
            Protections[e] = 0;
            ProtectionsNegative[e] = 0;
        }

        _onTimeEffects = new Dictionary<int, PlayerOnTimeAppliedEffect>();
        _effectsTerminable = new Dictionary<int, EffectTerminable>();
        _onTimeEffectsToRemove = new List<int>();
        CurrentState = State.Waiting;
        NextState = State.Waiting;
        _shields = new LinkedList<Shield>();
        IdAreaAppliedThisTurn = new List<int>();
    }

    public Character (int lifeMax, Hexagon position, GameObject go) : base(position)
	{
        _gameObject = GameObject.Instantiate(go);
        _gameObject.transform.position = position.GameObject.transform.position + new Vector3(0, 0.0f, 0);
        _gameObject.GetComponent<CharacterBehaviour>()._character = this;

		Protections = new Dictionary<Element, int> ();
		ProtectionsNegative = new Dictionary<Element, int> ();

		/*GlobalProtection = 0;
		GlobalNegativeProtection = 0;*/

		SommeProtection = 0;
		SommeNegativeProtection = 0;
        _globalShieldValue = 0;

        _lifeMax = lifeMax;
		_lifeCurrent = lifeMax;

        _rangeModifier = 0;
        DamageModifier = 0;
        HealModifier = 0;
        GlobalProtectionModifier = 0;

        _currentActionPoints = 1;
        _currentMovementPoints = 1;
        _turnNumber = 1;
        IsStabilized = false;

        foreach (var e in Element.GetElements()) {
			Protections [e] = 0;
			ProtectionsNegative [e] = 0;
		}

        _onTimeEffects = new Dictionary<int, PlayerOnTimeAppliedEffect>();
        _effectsTerminable = new Dictionary<int, EffectTerminable>();
        _onTimeEffectsToRemove = new List<int>();
        _shields = new LinkedList<Shield>();
        CurrentState = State.Waiting;
        NextState = State.Waiting;
        IdAreaAppliedThisTurn = new List<int>();
    }

    public void UpdateEffectTerminable()
    {
        List<int> toRemove = new List<int>();
        foreach(var effect in EffectsTerminable)
        {
            effect.Value.NbTurn--;
            Logger.Debug(effect.Value.NbTurn);
            if (effect.Value.NbTurn < 1)
            {
                List<Hexagon> list = new List<Hexagon>();
                list.Add(Position);
                effect.Value.ApplyEffect(list, Position, this);
                toRemove.Add(effect.Key);
            }
        }
        foreach (var id in toRemove)
        {
            EffectsTerminable.Remove(id);
        }
        
    }

    public void Copy(Character c)
    {
        _lifeCurrent = c._lifeCurrent;
        CurrentActionPoints = c.CurrentActionPoints;
        CurrentMovementPoints = c.CurrentMovementPoints;
        Name = c.Name;
        TurnNumber = c.TurnNumber;

        Protections = c.Protections;
        ProtectionsNegative = c.ProtectionsNegative;

        /*GlobalProtection = c.GlobalProtection;
        GlobalNegativeProtection = c.GlobalNegativeProtection;*/

        SommeProtection = c.SommeProtection;
        SommeNegativeProtection = c.SommeNegativeProtection;
    }

    /// <summary>
    /// Function that has to be called at the begining of the character's turn, reset some values and apply effects.
    /// </summary>
    public void BeginTurn()
    {
        Logger.Debug("begin turn character");
        //RangeModifier = 0; DONE
        //HealModifier = 0; DONE
       // DamageModifier = 0; DONE
        IsStabilized = false;

        /*foreach (var element in Element.GetElements())
        {
            ProtectionsNegative[element] = 0;
            Protections[element] = 0;
        }*/ //DONE
        IdAreaAppliedThisTurn = new List<int>(Position._onTimeEffects.Keys);

        Logger.Debug("begin update shield character");
        updateShields();
        Logger.Debug("end begin turn character");

        UpdateEffectTerminable();
    }

    public void updateShields()
    {
        LinkedListNode<Shield> node = _shields.First;
        while (node != null)
        {
            LinkedListNode<Shield> nextNode = node.Next;
            if (--(node.Value.NumberTurn) == 0)
            {
                _shields.Remove(node);
                _globalShieldValue -= node.Value.ShieldValue;
                EffectUIManager.GetInstance().AddTextEffect(this, new TextShieldLoss(node.Value.ShieldValue));
            }
            node = nextNode;
        }
    }

    public void ReceiveShield(Shield shield)
    {
        LinkedListNode<Shield> node = _shields.First;
        while(node != null)
        {
            if (node.Value.GetId() == shield.GetId())
            {
                _shields.Remove(node);
                _globalShieldValue -= node.Value.ShieldValue;
                EffectUIManager.GetInstance().AddTextEffect(this, new TextShieldLoss(node.Value.ShieldValue));
                break;
            }
            node = node.Next;
        }
        _shields.AddLast(new Shield(shield));
        _globalShieldValue += shield.ShieldValue;
        EffectUIManager.GetInstance().AddTextEffect(this, new TextShieldGain(shield.ShieldValue));
    }



    public void ReceiveRangeUp(int value)
    {
        Logger.Debug("Receive range up : " + value);
        EffectUIManager.GetInstance().AddTextEffect(this, new TextRangeGain(value));
        RangeModifier += value;
    }

    public void ReceiveRangeDown(int value)
    {
        Logger.Debug("Receive range down : " + value);
        EffectUIManager.GetInstance().AddTextEffect(this, new TextRangeLoss(value));
        RangeModifier -= value;
    }

    public void ReceiveHeal(int value)
    {
        value += HealModifier;
        
        if (_lifeCurrent + value > _lifeMax)
            _lifeCurrent = _lifeMax;
        else
            _lifeCurrent += value;

        Logger.Debug("Receive heal value : " + value);
        EffectUIManager.GetInstance().AddTextEffect(this, new TextHeal(value));
        HistoricManager.GetInstance().AddText(String.Format(StringsErule.heal, Name, value));
    }

    public int ShieldReceiveDamage(int value)
    {
        int firstValue = value;
        int nbShields = _shields.Count;
        while(value != 0 && _shields.Count > 0)
        {
            int min = Math.Min(value, _shields.Last.Value.ShieldValue);
            _shields.Last.Value.ShieldValue -= min;
            _globalShieldValue -= min;
            value -= min;
            if (_shields.Last.Value.ShieldValue == 0)
                _shields.RemoveLast();
        }
        if(nbShields > 0)
            EffectUIManager.GetInstance().AddTextEffect(this, new TextShieldLoss(firstValue - value));
        return value;
    }

    public int ReceiveDamage(int value, Element element)
    {
        
        int positiveElementResistance;
        int negativeElementResistance;

        Protections.TryGetValue(element, out positiveElementResistance);
        ProtectionsNegative.TryGetValue(element, out negativeElementResistance);

        //remove shield before apply element damages

        if(element!=Element.GetElement(5))
            value = ShieldReceiveDamage(value);

        int finalValue = (positiveElementResistance - negativeElementResistance);// + ((GlobalProtection - GlobalNegativeProtection)+GlobalProtectionModifier);
        float percentage = (100 - finalValue) / 100.0f;
        value = (int)(value * percentage);

        if (value > 0)
        {
            EffectUIManager.GetInstance().AddTextEffect(this, new TextDamage(value, element));
            HistoricManager.GetInstance().AddText(String.Format(StringsErule.damage, Name, value, element._name));
            GameObject.GetComponent<Animator>().SetTrigger("Hit");
        }

        if (_lifeCurrent - value < 0)
            _lifeCurrent = 0;
        else
            _lifeCurrent -= value;

        Logger.Debug("Receive damage value : " + value + " for element : " + element._name);
        _lifeMax -= (int)(0.25 * value);
        
        if(_lifeCurrent == 0)
        {
            PlayDead();
        }


        return value;
    }

	/*public void ReceiveGlobalProtection(int protection){

        EffectUIManager.GetInstance().AddTextEffect(this, new TextResistanceGain(protection, Element.GetElement(5)));

        Logger.Debug("Receive global protection : " + protection);

        int val = Math.Min (protection, GlobalNegativeProtection);

		GlobalNegativeProtection -= val;
		SommeNegativeProtection -= val;

		protection -= val;

		int max = MaxProtection - SommeProtection;

		GlobalProtection += Math.Min (max, protection);
		SommeProtection += Math.Min (max, protection);
	}*/

	/*public void ReceiveGlobalNegativeProtection(int protection){

        EffectUIManager.GetInstance().AddTextEffect(this, new TextResistanceLoss(protection, Element.GetElement(5)));

        Logger.Debug("Receive global negative protection : " + protection);

        int val = Math.Min (protection, GlobalProtection);

		GlobalProtection -= val;
		SommeProtection -= val;

		protection -= val;

		int max = MaxProtection - SommeNegativeProtection;

		GlobalNegativeProtection += Math.Min (max, protection);
		SommeNegativeProtection += Math.Min (max, protection);
	}*/

	public void ReceiveElementProtection(int protection, Element element){

        EffectUIManager.GetInstance().AddTextEffect(this, new TextResistanceGain(protection, element));

        Logger.Debug("Receive element protection : " + protection + " for element : " + element._name);

        /*int val = Math.Min (protection, ProtectionsNegative[element]);

		ProtectionsNegative[element] -= val;
		SommeNegativeProtection -= val;

		protection -= val;

		int max = MaxProtection - SommeProtection;

		Protections[element] += Math.Min (max, protection);
		SommeProtection += Math.Min (max, protection);
        Logger.Trace("nouvelle valeur : " + SommeProtection);*/

        Protections[element] = Mathf.Min(Protections[element] + protection, 100);
        Logger.Trace("Protection " + element.ToString() + " new value : " + Protections[element]);
	}

	public void ReceiveElementNegativeProtection(int protection, Element element){

        EffectUIManager.GetInstance().AddTextEffect(this, new TextResistanceLoss(protection, element));

        Logger.Debug("Receive element negative protection : " + protection + " for element : " + element._name);

        /*int val = Math.Min (protection, Protections[element]);

		Protections[element] -= val;
		SommeProtection -= val;

		protection -= val;

		int max = MaxProtection - SommeNegativeProtection;

		ProtectionsNegative[element] += Math.Min (max, protection);
		SommeNegativeProtection += Math.Min (max, protection);*/

        ProtectionsNegative[element] = Mathf.Min(ProtectionsNegative[element] + protection, 100);
        Logger.Trace("Protection " + element.ToString() + " new value : " + Protections[element]);
    }

    /// <summary>
    /// Adds a PlayerOnTimeAppliedEffect to the Character.
    /// </summary>
    /// <param name="effect">The effect to affect the Character by.</param>
    public void ReceiveOnTimeEffect(PlayerOnTimeAppliedEffect effect)
    {
        _onTimeEffects[effect.GetId()] = effect;

        if(effect._effect is DamageElement)
        {
            DamageElement de = (DamageElement)effect._effect;
            HistoricManager.GetInstance().AddText(String.Format(StringsErule.dot,Name,de._element._name,effect._nbTurn));
        }

        if (effect._effect is Heal)
        {
            HistoricManager.GetInstance().AddText(String.Format(StringsErule.hot, Name, effect._nbTurn));
        }
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

    public void ReceiveDamageUp(int value)
    {
        EffectUIManager.GetInstance().AddTextEffect(this, new TextDamageGain(value));
        DamageModifier += value;
    }

    public void ReceiveDamageDown(int value)
    {
        EffectUIManager.GetInstance().AddTextEffect(this, new TextDamageLoss(value));
        DamageModifier -= value;
    }
    public void ReceiveHealUp(int value)
    {
        EffectUIManager.GetInstance().AddTextEffect(this, new TextHealGain(value));
        HealModifier += value;
    }
    public void ReceiveHealDown(int value)
    {
        EffectUIManager.GetInstance().AddTextEffect(this, new TextHealLoss(value));
        HealModifier -= value;
    }

    public void ReceiveProtectionGlobalUp(int value)
    {
        GlobalProtectionModifier += value;
    }
    public void ReceiveProtectionGlobalDown(int value)
    {
        GlobalProtectionModifier -= value;
    }

    public void ReceiveRangeModifier(int value)
    {
        if (value > 0)
            ReceiveRangeUp(value);
        else
            ReceiveRangeDown(-value);
    }

    public void ReceiveStabilization()
    {
        IsStabilized = true;
    }

    public void ReceiveMovementUp(int value)
    {
        Logger.Debug("Receive movement up : " + value);
        EffectUIManager.GetInstance().AddTextEffect(this, new TextMovementGain(value));
        CurrentMovementPoints += value;
    }

    public void ReceiveMovementDown(int value)
    {
        Logger.Debug("Receive movement down : " + value);
        EffectUIManager.GetInstance().AddTextEffect(this, new TextMovementLoss(value));
        CurrentMovementPoints += value;
    }

    public int GetElementResistance(Element elem)
    {
        int res = 0;
        Protections.TryGetValue(elem, out res);
        return res;
    }

    public int GetElementWeakness(Element elem)
    {
        int res = 0;
        ProtectionsNegative.TryGetValue(elem, out res);
        return res;
    }

    /*public int GetGlobalResistance()
    {
        return GlobalProtection;
    }

    public int GetGlobalWeakness()
    {
        return GlobalNegativeProtection;
    }*/

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
        {
            return _position;
        }

        Hexagon target = _position.GetHexa(direction);
        if (!target.isReachable())
        {
            return _position;
        }
        else
        {
            _position._entity = null;
            target._entity = this;
            _position = target;

            return TranslateCharacter(direction, count - 1);
        }
    }

    public void PlayDead()
    {
        NextState = State.Dead;
        /*Character chara = PlayBoardManager.GetInstance().GetCurrentPlayer();
        if (chara == this)
            chara = PlayBoardManager.GetInstance().GetOtherPlayer();
        chara.CharacterState = State.Victorious;*/
    }

    /// <summary>
    /// Verify if the current chracter is on an hexagon that contains a portal. If so, it teleports the current character to the other portal (if it exists).
    /// </summary>
    /*public void Teleport()
    {
        List<Portal> portals = PlayBoardManager.GetInstance().Board.Portals;
        if (portals.Count == 2 && Position.Portal != null)
        {
            int indexBegin = portals.IndexOf(Position.Portal);
            int indexEnd = -1;
            switch (indexBegin)
            {
                case 0:
                    indexEnd = 1;
                    break;
                case 1:
                    indexEnd = 0;
                    break;
                default:
                    break;
            }

            Portal endPortal = portals[indexEnd];
            if (endPortal.Position._entity == null)
            {
                Position = endPortal.Position;
                endPortal.Destroy();
                portals.RemoveAt(indexEnd);

                _gameObject.transform.position = Position.GameObject.transform.position + PositionOffset;
            }
        }
    }*/

    public bool IsCharacterGetDot()
    {
        foreach(PlayerOnTimeAppliedEffect onTime in _onTimeEffects.Values)
        {
            if(onTime._effect is DamageElement)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsCharacterGetHot()
    {
        foreach (PlayerOnTimeAppliedEffect onTime in _onTimeEffects.Values)
        {
            if (onTime._effect is Heal)
            {
                return true;
            }
        }
        return false;
    }

    public bool isDead()
    {
        return _lifeCurrent <= 0;
    }

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

    public Dictionary<Element, int> Protections
    {
        get
        {
            return _protections;
        }

        set
        {
            _protections = value;
        }
    }

    public Dictionary<Element, int> ProtectionsNegative
    {
        get
        {
            return _protectionsNegative;
        }

        set
        {
            _protectionsNegative = value;
        }
    }

    /*public int GlobalProtection
    {
        get
        {
            return _globalProtection;
        }

        set
        {
            _globalProtection = value;
        }
    }

    public int GlobalNegativeProtection
    {
        get
        {
            return _globalNegativeProtection;
        }

        set
        {
            _globalNegativeProtection = value;
        }
    }*/

    public int SommeProtection
    {
        get
        {
            return _sumProtection;
        }

        set
        {
            _sumProtection = value;
        }
    }

    public int SommeNegativeProtection
    {
        get
        {
            return _sumNegativeProtection;
        }

        set
        {
            _sumNegativeProtection = value;
        }
    }

    public int CurrentMovementPoints
    {
        get
        {
            return _currentMovementPoints;
        }

        set
        {
            _currentMovementPoints = value;
        }
    }

    public int DamageModifier
    {
        get
        {
            return _damageModifier;
        }

        set
        {
            _damageModifier = value;
        }
    }

    public int HealModifier
    {
        get
        {
            return _healModifier;
        }

        set
        {
            _healModifier = value;
        }
    }

    public int GlobalProtectionModifier
    {
        get
        {
            return _globalProtectionModifier;
        }

        set
        {
            _globalProtectionModifier = value;
        }
    }

    public bool IsStabilized
    {
        get
        {
            return _isStabilized;
        }

        set
        {
            _isStabilized = value;
        }
    }

    public Vector3 PositionOffset
    {
        get
        {
            return _positionOffset;
        }

        set
        {
            _positionOffset = value;
        }
    }

    public int GlobalShieldValue
    {
        get
        {
            return _globalShieldValue;
        }

        set
        {
            _globalShieldValue = value;
        }
    }

    public Dictionary<int, EffectTerminable> EffectsTerminable
    {
        get
        {
            return _effectsTerminable;
        }

        set
        {
            _effectsTerminable = value;
        }
    }

    public bool GetDot
    {
        get
        {
            return _getDot;
        }

        set
        {
            _getDot = value;
        }
    }

    public bool GetHot
    {
        get
        {
            return _getHot;
        }

        set
        {
            _getHot = value;
        }
    }

    public State CurrentState
    {
        get
        {
            return _state;
        }

        set
        {
            _state = value;
            Animator anim = GameObject.GetComponent<Animator>();
            switch (_state)
            {
                case State.Waiting:
                    anim.SetBool("Idling", true);
                    anim.SetBool("Casting", false);
                    anim.SetBool("Victorious", false);
                    anim.SetBool("Defeated", false);
                    break;
                case State.CastingSpell:
                    anim.SetBool("Idling", false);
                    anim.SetBool("Casting", true);
                    anim.SetBool("Victorious", false);
                    anim.SetBool("Defeated", false);
                    break;
                case State.Victorious:
                    anim.SetBool("Idling", false);
                    anim.SetBool("Casting", false);
                    anim.SetBool("Victorious", true);
                    anim.SetBool("Defeated", false); 
                    break;
                case State.Dead:
                    anim.SetBool("Idling", false);
                    anim.SetBool("Casting", false);
                    anim.SetBool("Victorious", false);
                    anim.SetBool("Defeated", true);
                    break;
            }
        }
    }

    public State NextState
    {
        get
        {
            return _nextState;
        }

        set
        {
            _nextState = value;
        }
    }

    public List<int> IdAreaAppliedThisTurn
    {
        get
        {
            return _idAreaAppliedThisTurn;
        }

        set
        {
            _idAreaAppliedThisTurn = value;
        }
    }
}
