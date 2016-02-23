using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexagonBehaviour : MonoBehaviour
{
    public Hexagon _hexagon;

    private List<Hexagon> _finalArea;

    public List<Hexagon> FinalArea
    {
        get
        {
            return _finalArea;
        }

        set
        {
            _finalArea = value;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_hexagon.StateChanged)
        {
            switch(_hexagon.CurrentState)
            {
                case Hexagon.State.Default :
                    if (_hexagon.BoostElement == Hexagon.Boost.Nothing)
                        _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._default;
                    break;
                case Hexagon.State.Targetable:
                    _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._targetable;
                    break;
                case Hexagon.State.OverEnnemiTargetable:
                    _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._overEnnemiTargetable;
                    break;
                case Hexagon.State.OverSelfTargetable:
                    _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._overSelfTargetable;
                    break;
                case Hexagon.State.Accessible:
                    _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._accessible;
                    break;
                case Hexagon.State.OverAccessible:
                    _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._overAccessible;
                    break;
                case Hexagon.State.Spawnable:
                    _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._spawn;
                    break;
            }
            if (_hexagon._onTimeEffects.Count > 0)
                _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = HexagonColor._groundEffectColor;
            
            _hexagon.StateChanged = false;
        }

        if (Input.GetMouseButtonDown(0) && PlayBoardManager.GetInstance().GetCurrentPlayer()._state != Character.State.Moving)
        {
                             
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, ray.direction * 20);
            RaycastHit rch;
            //int layermask = (1 << LayerMask.NameToLayer("Default"));
            int layermask = LayerMask.GetMask("Hexagon");

            if (Physics.Raycast(ray, out rch, Mathf.Infinity, layermask))
            {
                Hexagon hexa = rch.collider.gameObject.GetComponent<HexagonBehaviour>()._hexagon;
                if (hexa != null && hexa.Equals(_hexagon) && (_hexagon.CurrentState == Hexagon.State.OverEnnemiTargetable || _hexagon.CurrentState == Hexagon.State.OverSelfTargetable
                    || _hexagon.CurrentState == Hexagon.State.Targetable))
                {
                    if (FinalArea == null)
                    {
                        Make_finalArea();
                    }
                    SpellManager.getInstance().ApplyEffects(FinalArea, hexa);
                    PlayBoardManager.GetInstance().Board.ResetBoard();
                    //SpellManager.getInstance().InitRange();
                }
            }
            
        }
    }


    /*void OnMouseEnter()
    {
        if (_hexagon.CurrentState == Hexagon.State.Targetable)
        {
            Make_finalArea();
        }
        if (_hexagon.CurrentState == Hexagon.State.Accessible)
            _hexagon.CurrentState = Hexagon.State.OverAccessible;
    }*/

    public void Make_finalArea()
    {
        if (_hexagon == PlayBoardManager.GetInstance().GetCurrentPlayer().Position)
        {
            FinalArea = SpellManager.getInstance().CurrentSelfArea.AreaToHexa(Direction.EnumDirection.East, _hexagon);
            //Logger.Error("nb hexa final area : " + _finalArea.Count);
            for (int i = 0; i < FinalArea.Count; i++)
            {
                //_finalArea[i].PreviousColor = _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color;
                //_finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = Color.yellow;
                FinalArea[i].CurrentState = Hexagon.State.OverSelfTargetable;
            }
        }
        else
        {
            Direction.EnumDirection newDirection = Direction.GetDirection(PlayBoardManager.GetInstance().GetCurrentPlayer().Position, _hexagon);
            FinalArea = SpellManager.getInstance().CurrentTargetArea.AreaToHexa(newDirection, _hexagon);
            //Logger.Error("nb hexa final area : " + _finalArea.Count);
            for (int i = 0; i < FinalArea.Count; i++)
            {
                //_finalArea[i].PreviousColor = _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color;
                //_finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
                FinalArea[i].CurrentState = Hexagon.State.OverEnnemiTargetable;
            }
            //_hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
        }
    }

    /*void OnMouseExit()
    {
        if (_hexagon.CurrentState == Hexagon.State.OverAccessible)
            _hexagon.CurrentState = _hexagon.PreviousState;

        if ((_hexagon.CurrentState == Hexagon.State.OverSelfTargetable || _hexagon.CurrentState == Hexagon.State.OverEnnemiTargetable ||
             _hexagon.CurrentState == Hexagon.State.Targetable) && FinalArea != null)
        {
            for (int i = 0; i < FinalArea.Count; i++)
            {
                if(FinalArea[i].CurrentState == Hexagon.State.OverSelfTargetable || FinalArea[i].CurrentState == Hexagon.State.OverEnnemiTargetable)
                    FinalArea[i].CurrentState = FinalArea[i].PreviousState;
            }
        }
    }*/
}
