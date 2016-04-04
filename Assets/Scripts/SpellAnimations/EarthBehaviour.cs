using UnityEngine;
using System.Collections;

public class EarthBehaviour : MonoBehaviour {
    private Vector3 _initialPosition;
    private float _maxHeight;
    private float _speed;
    private bool _goingUp;
    private float _timeStayingUp;
    private Vector3 _maxPosition;

    private float _currentTime;

    // Use this for initialization
    void Start () {
        _goingUp = true;
        _currentTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        if (_goingUp)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _maxPosition, _speed);
            if (Vector3.SqrMagnitude(_maxPosition - transform.position) < 0.0001)
            {
                _goingUp = false;
            }
        }
        else
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _timeStayingUp)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _initialPosition, _speed);
                if (Vector3.SqrMagnitude(transform.position - InitialPosition) < 0.0001)
                {
                    Destroy(gameObject);
                }
            }
        }

    }

    public Vector3 InitialPosition
    {
        get
        {
            return _initialPosition;
        }

        set
        {
            _initialPosition = value;
        }
    }

    public float MaxHeight
    {
        get
        {
            return _maxHeight;
        }

        set
        {
            _maxHeight = value;
            _maxPosition = _initialPosition - value * transform.up;//new Vector3(0, value, 0);
        }
    }

    public float Speed
    {
        get
        {
            return _speed;
        }

        set
        {
            _speed = value;
        }
    }

    public float TimeStayingUp
    {
        get
        {
            return _timeStayingUp;
        }

        set
        {
            _timeStayingUp = value;
        }
    }
}
