using UnityEngine;
using System.Collections;

public class Portal {

    private GameObject _gameObject;
    private Hexagon _position;

    public Portal()
    {

    }

    public Portal(Hexagon position, GameObject gameObject)
    {
        position.Portal = this;
        _gameObject = gameObject;
        _position = position;
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

    public Hexagon Position
    {
        get
        {
            return _position;
        }

        set
        {
            _position = value;
        }
    }

    public void Destroy()
    {
        _position.Portal = null;
        GameObject.Destroy(_gameObject);
    }
}
