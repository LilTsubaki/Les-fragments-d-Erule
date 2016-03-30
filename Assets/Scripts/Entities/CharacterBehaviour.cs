using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBehaviour : MonoBehaviour
{
    public Character _character;

    public float _movementSpeed;
    public float _translateSpeed;

    private List<Hexagon> finalArea;

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

        if (Input.GetMouseButtonDown(0) && PlayBoardManager.GetInstance().isMyTurn(_character) && _character.CharacterState != Character.State.Moving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawLine(ray.origin, ray.direction * 20);
            RaycastHit rch;
            //int layermask = (1 << LayerMask.NameToLayer("Default"));
            int layermask = LayerMask.GetMask("Hexagon");
           
            if (Physics.Raycast(ray, out rch, Mathf.Infinity, layermask))
            {
                HexagonBehaviour hexagonBehaviour = rch.collider.gameObject.GetComponent<HexagonBehaviour>();
                //MakeSpell(hexagonBehaviour);
                Hexagon hexa = hexagonBehaviour._hexagon;
                if (hexa != null)
                {
                    bool pathFound = PlayBoardManager.GetInstance().Board.FindPathForCharacter(_character, hexa);
                    _character.CurrentStep = 0;

                    if (pathFound && _character.PathToFollow.Count > 0)
                        _character.CharacterState = Character.State.Moving;
                }
            }
        }

        switch (_character.CharacterState)
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
        Vector3 temp = _character.PositionOffset;
        transform.position = Vector3.MoveTowards(transform.position, hexa.GameObject.transform.position + temp, step);
        return Mathf.Approximately(Vector3.SqrMagnitude(hexa.GameObject.transform.position + temp - transform.position), 0);
    }

    void Translate()
    {
        if(goTo(_character.Position, _translateSpeed))
        {
            _character.CharacterState = Character.State.Waiting;
            // Teleport player if the last hexagon has a portal
            if (_character.Position.Portal != null)
            {
                PortalManager.GetInstance().Teleport(_character);
            }
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
                _character.CurrentStep++;
                if(_character.CurrentStep == _character.PathToFollow.Count)
                {
                    _character.Position = _character.PathToFollow[0];
                    _character.CharacterState = Character.State.Waiting;
                    PlayBoardManager.GetInstance().Board._colorAccessible = true;
                    // Teleport player if the last hexagon has a portal
                    if (_character.Position.Portal != null)
                    {
                        PortalManager.GetInstance().Teleport(_character);
                    }
                }
            }
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
