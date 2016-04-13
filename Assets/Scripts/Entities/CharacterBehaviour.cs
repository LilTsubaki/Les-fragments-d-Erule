﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBehaviour : MonoBehaviour
{
    public Character _character;

    public float _movementSpeed;
    public float _translateSpeed;

    private Hexagon _previousHexagon;
    private List<Hexagon> finalArea;

    public Orbs _orbs;
    public GameObject _castChannel;

    private Quaternion _nextRotation;

    private Character.State _stateBeforeMove;
    

    void Awake()
    {
        _movementSpeed = 4.0f;
        _translateSpeed = 8.0f;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ServerManager.GetInstance()._server.CurrentState == Server.State.playing)
        {
            if (Input.GetMouseButtonDown(0) && PlayBoardManager.GetInstance().isMyTurn(_character) && _character.CurrentState != Character.State.Moving && _character.CurrentState != Character.State.Rotating)
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
                        {
                            _stateBeforeMove = _character.CurrentState;
                            _character.NextState = Character.State.Rotating;
                            Direction.EnumDirection nextDirection = Direction.GetDirection(_character.Position, _character.PathToFollow[_character.PathToFollow.Count-1]);
                            float diff = Direction.GetDiffAngle(_character._direction, nextDirection);
                            _nextRotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y + diff, gameObject.transform.rotation.z);
                        }
                    }
                }
            }

            if (_character != null)
            {

                if (_character.NextState != _character.CurrentState)
                {
                    _character.PreviousState = _character.CurrentState;
                    _character.CurrentState = _character.NextState;
                }

                if (_character._changeOrbs)
                {
                    SetNewOrbs(_character._orbs);
                    _character._changeOrbs = false;
                }
            }

            switch (_character.CurrentState)
            {
                case Character.State.Rotating:
                    Rotate();
                    break;
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
    }
    
    void Rotate()
    {
        gameObject.transform.rotation = /*_nextRotation;//*/ Quaternion.RotateTowards(gameObject.transform.rotation, _nextRotation, 1);
        if (Quaternion.Dot(gameObject.transform.rotation, _nextRotation) > 0.999f)
        {
            _character.NextState = Character.State.Moving;
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
        if (goTo(_character.Position, _translateSpeed))
        {
            _character.NextState = _character.PreviousState;
            // Teleport player if the last hexagon has a portal
            if (_character.Position.Portal != null)
            {
                PortalManager.GetInstance().Teleport(_character);
            }
        }
    }

    void Move()
    {
        if (_character.PathToFollow != null && _character.PathToFollow.Count > 0)
        {
            if (_character.CurrentStep == 0)
            {
                PlayBoardManager.GetInstance().Board.ResetBoard();
            }
            if (_character.CurrentStep <= _character.PathToFollow.Count && goTo(_character.PathToFollow[_character.PathToFollow.Count - 1 - _character.CurrentStep], _movementSpeed))
            {
                Hexagon currentHexa = _character.PathToFollow[_character.PathToFollow.Count - 1 - _character.CurrentStep];
                _character.Position = currentHexa;

                List<int> idToKeep = new List<int>();
                foreach (int id in _character.IdAreaAppliedThisTurn)
                {
                    if (currentHexa._onTimeEffects.ContainsKey(id))
                    {
                        idToKeep.Add(id);
                    }
                }
                _character.IdAreaAppliedThisTurn = idToKeep;


                List<Hexagon> list = new List<Hexagon>();
                list.Add(currentHexa);
                foreach (var effect in currentHexa._onTimeEffects.Values)
                {
                    if (!_character.IdAreaAppliedThisTurn.Contains(effect.GetId()))
                    {
                        _character.IdAreaAppliedThisTurn.Add(effect.GetId());
                        effect.ApplyEffect(list, currentHexa, effect.GetCaster());
                    }
                }

                _character.CurrentStep++;

                if (_character.CurrentStep == _character.PathToFollow.Count)
                {
                    _character.Position = _character.PathToFollow[0];
                    _character.NextState = _stateBeforeMove;
                    PlayBoardManager.GetInstance().Board._colorAccessible = true;
                    // Teleport player if the last hexagon has a portal
                    if (_character.Position.Portal != null)
                    {
                        PortalManager.GetInstance().Teleport(_character);
                    }

                }
                else
                {
                    _character.NextState = Character.State.Rotating;
                    Direction.EnumDirection nextDirection = Direction.GetDirection(currentHexa, _character.PathToFollow[_character.PathToFollow.Count - 1 - _character.CurrentStep]);
                    float diff = Direction.GetDiffAngle(_character._direction, nextDirection);
                    _nextRotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y + diff, gameObject.transform.rotation.z);
                }
            }
        }
    }

    public void ActivateMesh(int boolean)
    {
        bool active = boolean == 0 ? false : true;
        gameObject.transform.GetChild(0).gameObject.SetActive(active);
        gameObject.transform.GetChild(1).gameObject.SetActive(active);
        gameObject.transform.GetChild(2).gameObject.SetActive(active);
        gameObject.transform.GetChild(3).gameObject.SetActive(active);
    }

    public void SetNewOrbs(List<Element> elems)
    {
        _orbs.SetElements(elems);
    }

    public void CastSpellAnimations()
    {
        SpellAnimationManager.GetInstance().PlaySavedCast();
    }

    public void CastSelfSpellAnimations()
    {
        SpellAnimationManager.GetInstance().PlaySavedSelfCast();
    }

    public void MakeOrbsDisappear(int appearing)
    {
        bool appear = appearing == 1 ? true : false;
        _castChannel.SetActive(!appear);
        _orbs.ActivateRunes(appear);
    }
}
