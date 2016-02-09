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

    }

    void OnMouseEnter()
    {
        if (_hexagon.Targetable)
        {
            if(_hexagon == PlayBoardManager.GetInstance().GetCurrentPlayer().Position)
            {
                List<Element> elementsList = RunicBoardManager.GetInstance().GetBoardPlayer1().GetSortedElementList();
                Queue<Element> elements = new Queue<Element>(elementsList);
                SelfSpell self = SpellManager.getInstance().ElementNode.GetSelfSpell(elements);

                Area area = SpellManager.getInstance().GetAreaById(self.AreaId);
                //Direction.EnumDirection newDirection = Direction.GetLineDirection(PlayBoardManager.GetInstance().GetCurrentPlayer().Position, _hexagon);

                finalArea = area.AreaToHexa(Direction.EnumDirection.East, _hexagon);
                //Logger.Error("nb hexa final area : " + finalArea.Count);
                for (int i = 0; i < finalArea.Count; i++)
                {
                    //finalArea[i].PreviousColor = _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color;
                    finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = Color.yellow;
                }
            }
            else
            {
                List<Element> elementsList = RunicBoardManager.GetInstance().GetBoardPlayer1().GetSortedElementList();
                Queue<Element> elements = new Queue<Element>(elementsList);
                TargetSpell testTsp = SpellManager.getInstance().ElementNode.GetTargetSpell(elements);

                Area area = SpellManager.getInstance().GetAreaById(testTsp.AreaId);
                Direction.EnumDirection newDirection = Direction.GetDirection(PlayBoardManager.GetInstance().GetCurrentPlayer().Position, _hexagon);
                Logger.Debug("new direction : " + newDirection);

                finalArea = area.AreaToHexa(newDirection, _hexagon);
                //Logger.Error("nb hexa final area : " + finalArea.Count);
                for (int i = 0; i < finalArea.Count; i++)
                {
                    //finalArea[i].PreviousColor = _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color;
                    finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
                }
                //_hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
            }
        }
    }

    void OnMouseExit()
    {
        if (_hexagon.Targetable)
        {
            for (int i = 0; i < finalArea.Count; i++)
            {
                finalArea[i].GameObject.GetComponentInChildren<Renderer>().material.color = finalArea[i].PreviousColor;
            }
        }
    }
}
