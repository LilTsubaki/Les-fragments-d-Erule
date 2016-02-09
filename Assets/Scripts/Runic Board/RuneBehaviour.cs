using UnityEngine;
using System.Collections;

public class RuneBehaviour : MonoBehaviour {

    internal enum State { Static, Held, BeingReleased }

    internal Rune _rune;
    internal State _state;
    
    internal Transform _initialParent;

    private float _runeSpeed;
    // Runes move on this plane
    private Plane _plane;
    
    void Awake()
    {
        _runeSpeed = 10.0f;
        _plane = new Plane(gameObject.transform.up, gameObject.transform.position + new Vector3(0, 0.15f, 0));
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        switch(_state)
        {
            case State.Held:
                Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float distance;
                if (_plane.Raycast(camRay, out distance))
                {
                    gameObject.transform.position = camRay.GetPoint(distance);
                }
                break;
            case State.BeingReleased:
                float step = _runeSpeed * Time.deltaTime;
                transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(0,0.3f,0), step);
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
