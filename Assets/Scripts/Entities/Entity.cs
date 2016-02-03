using UnityEngine;
using System.Collections;

public class Entity {
    protected Hexagon _position;

    public Hexagon Position
    {
        get { return _position; }
        set { _position = value; }
    }


    public Entity(Hexagon position)
    {
        _position = position;
        position._entity = this;
    }
}
