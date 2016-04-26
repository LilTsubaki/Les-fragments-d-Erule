using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexagonBehaviour : MonoBehaviour
{
    public Hexagon _hexagon;
    public string _hexagonType;

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
        if (_hexagon.GroundEffect != null && _hexagon.ShardEffect != null)
        {
            
            
            if (_hexagon._onTimeEffects.Count > 0)
            {
                _hexagon.ShardEffect.SetActive(false);
                _hexagon.GroundEffect.SetActive(true);
            }
            else if(_hexagon.IsActiveShardAround())
            {
                _hexagon.GroundEffect.SetActive(false);
                _hexagon.ShardEffect.SetActive(true);
            }
            else
            {
                _hexagon.GroundEffect.SetActive(false);
                _hexagon.ShardEffect.SetActive(false);
            }
        }

            if (_hexagon.StateChanged)
        {
            switch (_hexagon.CurrentState)
            {
                case Hexagon.State.Default :
                    _hexagon.Glyph.SetActive(false);
                    break;
                case Hexagon.State.Targetable:
                    _hexagon.Glyph.SetActive(true);
                    _hexagon.Glyph.GetComponentInChildren<Renderer>().material.color = ColorErule._targetable;

                    break;
                case Hexagon.State.OverEnnemiTargetable:
                    _hexagon.Glyph.SetActive(true);
                    _hexagon.Glyph.GetComponentInChildren<Renderer>().material.color = ColorErule._overEnnemiTargetable;

                    break;
                case Hexagon.State.OverSelfTargetable:
                    _hexagon.Glyph.SetActive(true);
                    _hexagon.Glyph.GetComponentInChildren<Renderer>().material.color = ColorErule._overSelfTargetable;

                    break;
                case Hexagon.State.Accessible:
                    _hexagon.Glyph.SetActive(true);
                    _hexagon.Glyph.GetComponentInChildren<Renderer>().material.color = ColorErule._accessible;

                    break;
                case Hexagon.State.OverAccessible:
                    _hexagon.Glyph.SetActive(true);
                    _hexagon.Glyph.GetComponentInChildren<Renderer>().material.color = ColorErule._overAccessible;

                    break;
                case Hexagon.State.Spawnable:
                    _hexagon.Glyph.SetActive(true);
                    _hexagon.Glyph.GetComponentInChildren<Renderer>().material.color = ColorErule._spawn;

                    break;
            }
            
            _hexagon.StateChanged = false;
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

    public void MakeFinalArea()
    {
        if (_hexagon == PlayBoardManager.GetInstance().GetCurrentPlayer().Position)
        {
            FinalArea = SpellManager.GetInstance().CurrentSelfArea.AreaToHexa(Direction.EnumDirection.East, _hexagon);
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
            FinalArea = SpellManager.GetInstance().CurrentTargetArea.AreaToHexa(newDirection, _hexagon);
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
