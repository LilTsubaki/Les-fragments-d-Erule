using UnityEngine;
using System.Collections;
using System;

public abstract class Entity {
    protected Hexagon _position;
    protected GameObject _gameObject;

    protected int _damageBuffer;

    public Hexagon Position
    {
        get { return _position; }
        set
        {
            if (_position != null)
            {
                _position._entity = null;
            }

            _position = value;

            if (_position != null)
            {
                _position._entity= this;
            }
        }
    }

    public GameObject GameObject
    {
        get
        {
            return _gameObject;
        }

        set
        {
            _gameObject = value;
        }
    }

    public int DamageBuffer
    {
        get
        {
            return _damageBuffer;
        }

        set
        {
            _damageBuffer = value;
        }
    }

    public Entity()
    {

    }

    public Entity(Hexagon position)
    {
        if (position == null)
            return;

        if (!Hexagon.isHexagonSet(position) || position._entity != null)
            throw new Exception("Invalid Position");
        _position = position;
        position._entity = this;
    }

    public int UnloadDamagerBuffer()
    {
        int dmg = _damageBuffer;
        Logger.Debug("Total dmg : " +dmg);
        _damageBuffer = 0;
        return dmg;
    }
}
