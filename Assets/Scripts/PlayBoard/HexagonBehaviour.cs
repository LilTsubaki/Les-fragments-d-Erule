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
        if (_hexagon.Targetable)
        {
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
                    if (hexa != null && hexa.Equals(_hexagon))
                    {
                        SpellManager.getInstance().ApplyEffects(finalArea, hexa);
                        PlayBoardManager.GetInstance().Board.ResetBoard();
                        //SpellManager.getInstance().InitRange();
                    }
                }
            }
        }
    }


    void OnMouseEnter()
    {
        if (_hexagon.Targetable)
        {
            MakeFinalArea();
        }
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
                finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = Color.yellow;
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
                finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
            }
            //_hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        if (_hexagon.Targetable && finalArea != null)
        {
            for (int i = 0; i < finalArea.Count; i++)
            {
                finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = finalArea[i].PreviousColor;
            }
        }
    }
}
