using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexagonBehaviour : MonoBehaviour
{
    public Hexagon _hexagon;

    private List<Hexagon> finalArea;

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
            _hexagon.StateChanged = false;
        }


            if (Input.GetMouseButtonDown(0) && PlayBoardManager.GetInstance().GetCurrentPlayer()._state != Character.State.Moving)
            {
                if(finalArea == null)
                {
                    MakeFinalArea();
                }                
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
                        SpellManager.getInstance().ApplyEffects(finalArea, hexa);
                        PlayBoardManager.GetInstance().Board.ResetBoard();
                        //SpellManager.getInstance().InitRange();
                    }
                }
            
        }
    }


    void OnMouseEnter()
    {
        if (_hexagon.CurrentState == Hexagon.State.Targetable)
        {
            MakeFinalArea();
        }
        if (_hexagon.CurrentState == Hexagon.State.Accessible)
            _hexagon.CurrentState = Hexagon.State.OverAccessible;
    }

    public void MakeFinalArea()
    {
        if (_hexagon == PlayBoardManager.GetInstance().GetCurrentPlayer().Position)
        {
            finalArea = SpellManager.getInstance().CurrentSelfArea.AreaToHexa(Direction.EnumDirection.East, _hexagon);
            //Logger.Error("nb hexa final area : " + finalArea.Count);
            for (int i = 0; i < finalArea.Count; i++)
            {
                //finalArea[i].PreviousColor = _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color;
                //finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = Color.yellow;
                finalArea[i].CurrentState = Hexagon.State.OverSelfTargetable;
            }
        }
        else
        {
            Direction.EnumDirection newDirection = Direction.GetDirection(PlayBoardManager.GetInstance().GetCurrentPlayer().Position, _hexagon);
            finalArea = SpellManager.getInstance().CurrentTargetArea.AreaToHexa(newDirection, _hexagon);
            //Logger.Error("nb hexa final area : " + finalArea.Count);
            for (int i = 0; i < finalArea.Count; i++)
            {
                //finalArea[i].PreviousColor = _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color;
                //finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
                finalArea[i].CurrentState = Hexagon.State.OverEnnemiTargetable;
            }
            //_hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        if (_hexagon.CurrentState == Hexagon.State.OverAccessible)
            _hexagon.CurrentState = _hexagon.PreviousState;

        if ((_hexagon.CurrentState == Hexagon.State.OverSelfTargetable || _hexagon.CurrentState == Hexagon.State.OverEnnemiTargetable ||
             _hexagon.CurrentState == Hexagon.State.Targetable) && finalArea != null)
        {
            for (int i = 0; i < finalArea.Count; i++)
            {
                if(finalArea[i].CurrentState == Hexagon.State.OverSelfTargetable || finalArea[i].CurrentState == Hexagon.State.OverEnnemiTargetable)
                    finalArea[i].CurrentState = finalArea[i].PreviousState;
            }
        }
    }
}
