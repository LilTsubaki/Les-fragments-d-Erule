﻿using UnityEngine;
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

        if (Input.GetMouseButtonDown(0) && PlayBoardManager.GetInstance().isMyTurn(_character) && _character.CurrentState != Character.State.Moving)
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
                        _character.NextState = Character.State.Moving;
                }
            }
        }

        if(_character.NextState != _character.CurrentState)
        {
            _character.CurrentState = _character.NextState;
        }

        switch (_character.CurrentState)
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
            _character.NextState = Character.State.Waiting;
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
                Hexagon currentHexa = _character.PathToFollow[_character.PathToFollow.Count - 1 - _character.CurrentStep];
                //check if the player walk into an area
                if (currentHexa._onTimeEffects.Count > 0)
                {
                    for(int i = 0; i < currentHexa._onTimeEffects.Count; i++)
                    {
                        if(!_character.IdAreaAppliedThisTurn.Contains(currentHexa._onTimeEffects[i].GetId()))
                        {
                            List<Hexagon> list = new List<Hexagon>();
                            list.Add(currentHexa);
                            _character.IdAreaAppliedThisTurn.Add(currentHexa._onTimeEffects[i].GetId());
                            currentHexa._onTimeEffects[i].ApplyEffect(list, currentHexa, currentHexa._onTimeEffects[i].GetCaster());
                        }
                    }
                }

                _character.CurrentStep++;
                if(_character.CurrentStep == _character.PathToFollow.Count)
                {
                    _character.Position = _character.PathToFollow[0];
                    _character.NextState = Character.State.Waiting;
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

    public void ActivateMesh(int boolean)
    {
        bool active = boolean == 0 ? false : true;
        gameObject.transform.GetChild(0).gameObject.SetActive(active);
        gameObject.transform.GetChild(1).gameObject.SetActive(active);
    }
}
