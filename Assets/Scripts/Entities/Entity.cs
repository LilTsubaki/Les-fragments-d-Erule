using UnityEngine;
using System.Collections;
using System;

public class Entity {
    protected Hexagon _position;

    public Hexagon Position
    {
        get { return _position; }
        set {
            _position._entity = null;
            _position = value;
            _position._entity= this; }
    }

    public Entity()
    {

    }

    public Entity(Hexagon position)
    {

        if (!Hexagon.isHexagonSet(position) || position._entity != null)
            throw new Exception("Invalid Position");
            _position = position;
        position._entity = this;
        
    }
}
