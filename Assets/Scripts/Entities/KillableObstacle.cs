using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class KillableObstacle : Obstacle, Killable
{
    private int _maxLife;
    private int _currentLife;
    private Character _caster;
    private Dictionary<int, PlayerOnTimeAppliedEffect> _onTimeEffects;
    private List<int> _onTimeEffectsToRemove;


    public KillableObstacle(Hexagon position, int life, Character caster) : base(position)
    {
        MaxLife = life;
        CurrentLife = life;
        Caster = caster;
        _onTimeEffects = new Dictionary<int, PlayerOnTimeAppliedEffect>();
        _onTimeEffectsToRemove = new List<int>();
    }


    public int ReceiveDamage(int value, Element element)
    {
        _damageBuffer += value;

        if (_currentLife - value < 0)
            _currentLife = 0;
        else
            _currentLife -= value;

        Logger.Debug("Receive damage value : " + value + " for element : " + element._name);
        EffectUIManager.GetInstance().AddTextEffect(this, new TextDamage(value,element));
        return value;
    }

    public void ReceiveOnTimeEffect(PlayerOnTimeAppliedEffect effect)
    {
        if (effect._effect is DamageElement)
        {
            _onTimeEffects[effect.GetId()] = effect;
        }            
    }

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
            Logger.Trace("Applying OnTimeEffect Obstacle" + effect.GetId());
            effect.ApplyEffect(hexagons, _position, Caster);            
        }
    }


    public void RemoveMarkedOnTimeEffects()
    {
        foreach (int id in _onTimeEffectsToRemove)
        {
            _onTimeEffects.Remove(id);
        }
    }


    public bool isDead()
    {
        return CurrentLife <= 0;
    }

    public int MaxLife
    {
        get
        {
            return _maxLife;
        }

        set
        {
            _maxLife = value;
        }
    }

    public int CurrentLife
    {
        get
        {
            return _currentLife;
        }

        set
        {
            _currentLife = value;
        }
    }

    public Character Caster
    {
        get
        {
            return _caster;
        }

        set
        {
            _caster = value;
        }
    }
}

