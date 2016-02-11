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
                    bool pathFound = PlayBoardManager.GetInstance().Board.FindPathForCharacter(_character, hexa);
                    _character.CurrentStep = 0;

                    if (pathFound && _character.PathToFollow.Count > 0)
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

    void OnMouseEnter()
    {
        if (_character.Position.Targetable)
        {
            if (_character.Position == PlayBoardManager.GetInstance().GetCurrentPlayer().Position)
            {

                finalArea = SpellManager.getInstance().CurrentSelfArea.AreaToHexa(Direction.EnumDirection.East, _character.Position);
                //Logger.Error("nb hexa final area : " + finalArea.Count);
                for (int i = 0; i < finalArea.Count; i++)
                {
                    //finalArea[i].PreviousColor = _character.Position.GameObject.GetComponentInChildren<Renderer>().material.color;
                    finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = Color.yellow;
                }
            }
            else
            {
                Direction.EnumDirection newDirection = Direction.GetDirection(PlayBoardManager.GetInstance().GetCurrentPlayer().Position, _character.Position);
                finalArea = SpellManager.getInstance().CurrentTargetArea.AreaToHexa(newDirection, _character.Position);
                //Logger.Error("nb hexa final area : " + finalArea.Count);
                for (int i = 0; i < finalArea.Count; i++)
                {
                    //finalArea[i].PreviousColor = _character.Position.GameObject.GetComponentInChildren<Renderer>().material.color;
                    finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
                }
                //_character.Position.GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
            }
        }
    }

    void OnMouseExit()
    {
        if (_character.Position.Targetable)
        {
            for (int i = 0; i < finalArea.Count; i++)
            {
                finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = finalArea[i].PreviousColor;
            }
        }
    }
}
