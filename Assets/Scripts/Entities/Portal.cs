using UnityEngine;
using System.Collections;

public class Portal {

    private GameObject _gameObject;
    private Hexagon _position;
    private float _timestamp;
    private int _id;

    public Portal()
    {

    }

    public Portal(Hexagon position, GameObject gameObject, int id)
    {
        if (position != null)
        {
            position.Portal = this;
        }
        _gameObject = gameObject;
        _position = position;
        _id = id;
    }

    public bool IsActive()
    {
        return (_position != null);
    }

    public void ActivatePortal(Hexagon position)
    {
        Position = position;
        _gameObject.SetActive(true);
        _gameObject.transform.position = position.GameObject.transform.position + new Vector3(0, 1, 0);
        _timestamp = Time.time;
    }

    public void Destroy()
    {
        _position.Portal = null;
        _position = null;
        _gameObject.SetActive(false);
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
            _position.Portal = null;
            _position = value;
        }
    }

    public float Timestamp
    {
        get
        {
            return _timestamp;
        }

        set
        {
            _timestamp = value;
        }
    }
}
