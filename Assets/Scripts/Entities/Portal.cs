using UnityEngine;
using System.Collections;

public class Portal : Entity {

    private GameObject _gameObject;

    public Portal()
    {

    }

    public Portal(Hexagon position, GameObject gameObject) : base(position)
    {
        _gameObject = gameObject;
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

    public void Destroy()
    {
        GameObject.Destroy(_gameObject);
    }
}
