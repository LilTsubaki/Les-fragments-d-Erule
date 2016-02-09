using UnityEngine;
using System.Collections;

public class RuneBehaviour : MonoBehaviour {

    internal enum State { Static, Held, BeingReleased, BeingTaken }

    internal Rune _rune;
    internal State _state;
    
    internal Transform _initialParent;

    private float _runeSpeed;
    // Runes move on this plane
    private Plane _plane;
    // Local Position of the rune
    private Vector3 _upPosition;
    // Offset added to the local position when held
    private Vector3 _upOffsetWhenHeld;
    
    void Awake()
    {
        _runeSpeed = 10.0f;
        _upPosition = new Vector3(0, 0.3f, 0);
        _upOffsetWhenHeld = new Vector3(0, 0.15f, 0);
        _plane = new Plane(gameObject.transform.up, _upPosition + _upOffsetWhenHeld);
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float step;
        Ray camRay;
        float distance;

        switch (_state)
        {
            case State.BeingTaken:
                camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (_plane.Raycast(camRay, out distance))
                {
                    step = 4 * _runeSpeed * Time.deltaTime;
                    transform.position = Vector3.Slerp(transform.position, camRay.GetPoint(distance), step);
                    if (Vector3.SqrMagnitude(transform.position - camRay.GetPoint(distance)) < 0.0001)
                    {
                        _state = State.Held;
                    }
                }
                break;
            case State.Held:
                camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (_plane.Raycast(camRay, out distance))
                {
                    gameObject.transform.position = camRay.GetPoint(distance);
                }
                break;
            case State.BeingReleased:
                step = _runeSpeed * Time.deltaTime;
                transform.localPosition = Vector3.Slerp(transform.localPosition, _upPosition, step);
                //transform.position = Vector3.MoveTowards(transform.position, _initialPosition, Mathf.Lerp(0, Vector3.Distance(_initialPosition, transform.position), 0.1f));

                if (Vector3.SqrMagnitude(transform.localPosition - new Vector3(0, 0.3f, 0)) < 0.00001)
                {
                    _state = State.Static;
                }
                break;
            case State.Static:
                break;
            default:
                break;
        }
	}
}
