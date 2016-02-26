using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class KillableObstacle : Obstacle, Killable
{
    private int _maxLife;
    private int _currentLife;

    

    public KillableObstacle(Hexagon position, int life) : base(position)
    {
        MaxLife = life;
        CurrentLife = life;
    }


    public int ReceiveDamage(int value, Element element)
    {
        if (_currentLife - value < 0)
            _currentLife = 0;
        else
            _currentLife -= value;

        Logger.Debug("Receive damage value : " + value + " for element : " + element._name);
        return value;
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
}

