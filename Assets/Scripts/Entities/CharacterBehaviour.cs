using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour
{
    public Character _character;

    public float _movementSpeed;
    public float _translateSpeed;

    void Awake()
    {
        _movementSpeed = 4.0f;
        _translateSpeed = 8.0f;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(1) && TurnManager.GetInstance().isMyTurn(_character) && _character._state != Character.State.Moving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, ray.direction * 20);
            RaycastHit rch;
            //int layermask = (1 << LayerMask.NameToLayer("Default"));
            int layermask = LayerMask.GetMask("Hexagon");
           
            if (Physics.Raycast(ray, out rch, Mathf.Infinity, layermask))
            {
                Hexagon hexa = rch.collider.gameObject.GetComponent<HexagonBehaviour>()._hexagon;
                if (hexa != null)
                {
                    
                    PlayBoardManager.GetInstance().Board.FindPathForCharacter(_character, hexa);
                    _character.CurrentStep = 0;

                    if (_character.PathToFollow != null && _character.PathToFollow.Count > 0)
                        _character._state = Character.State.Moving;
                }
            }
        }

        /*if (TurnManager.GetInstance().isMyTurn(_character) && Input.GetKeyDown(KeyCode.Z))
        {
            _character.TranslateCharacter(Direction.EnumDirection.East, 12);
            _character._state = Character.State.Translating;
        }*/

        switch (_character._state)
        {
            case Character.State.Moving:
                Move();
                break;
            case Character.State.Translating:
                Translate();
                break;
            default:
                break;
        }
    }

    bool goTo(Hexagon hexa, float speed)
    {
        float step = speed * Time.deltaTime;
        Vector3 temp = new Vector3(0.0f, 0.5f, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, hexa.GameObject.transform.position + temp, step);
        return Mathf.Approximately(Vector3.SqrMagnitude(hexa.GameObject.transform.position + temp - transform.position), 0);
    }

    void Translate()
    {
        if(goTo(_character.Position, _translateSpeed))
        {
            _character._state = Character.State.Waiting;
        }
    }

    void Move()
    {
        if(_character.PathToFollow != null && _character.PathToFollow.Count > 0)
        {
            if(_character.CurrentStep == 0)
            {
                PlayBoardManager.GetInstance().Board.ResetBoard();
            }
            if (_character.CurrentStep <= _character.PathToFollow.Count && goTo(_character.PathToFollow[_character.PathToFollow.Count -1 - _character.CurrentStep], _movementSpeed))
            {
                //_character.PathToFollow[_character.PathToFollow.Count - 1 - _character.CurrentStep].onPlayerEnter(this);
                _character.CurrentStep++;
                if(_character.CurrentStep == _character.PathToFollow.Count)
                {
                    //fieldOfView();
                    _character.Position = _character.PathToFollow[0];
                    _character._state = Character.State.Waiting;
                }
            }
        }
    }
}
