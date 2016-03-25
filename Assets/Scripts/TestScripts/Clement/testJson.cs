using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testJson : MonoBehaviour
{
    private Dictionary<int, Area> _areas;
    private List<Hexagon> _finalArea;

    // Use this for initialization
    void Start()
    {
        _areas = new Dictionary<int, Area>();
        LoadArea();

        Area area = GetAreaById(7);
        PlayBoard playBoard = new PlayBoard(5, 5);

        List<Hexagon> hexas = new List<Hexagon>();
        GameObject glyph = Resources.Load<GameObject>("prefabs/SM_Glyphe_1");
        GameObject board = new GameObject("Board");

        hexas.Add(new Hexagon(0, 2, playBoard));
        hexas.Add(new Hexagon(1, 2, playBoard));
        hexas.Add(new Hexagon(2, 2, playBoard)); //centre
        hexas.Add(new Hexagon(3, 2, playBoard));
        hexas.Add(new Hexagon(4, 2, playBoard));

        hexas.Add(new Hexagon(1, 3, playBoard));
        hexas.Add(new Hexagon(2, 3, playBoard));
        hexas.Add(new Hexagon(3, 3, playBoard));
        hexas.Add(new Hexagon(4, 3, playBoard));

        hexas.Add(new Hexagon(2, 4, playBoard));
        hexas.Add(new Hexagon(3, 4, playBoard));
        hexas.Add(new Hexagon(4, 4, playBoard));

        hexas.Add(new Hexagon(0, 1, playBoard));
        hexas.Add(new Hexagon(1, 1, playBoard));
        hexas.Add(new Hexagon(2, 1, playBoard));
        hexas.Add(new Hexagon(3, 1, playBoard));

        hexas.Add(new Hexagon(0, 0, playBoard));
        hexas.Add(new Hexagon(1, 0, playBoard));
        hexas.Add(new Hexagon(2, 0, playBoard));

        for (int i = 0; i < hexas.Count; i++)
        {
            Hexagon hexagon = playBoard.CreateHexagone(hexas[i]._posX, hexas[i]._posY);
            hexagon.GameObject = new GameObject();
            hexagon.GameObject.transform.parent = board.transform;
             

            hexagon.Glyph = GameObject.Instantiate(glyph);
            hexagon.Glyph.transform.parent = hexagon.GameObject.transform;
            hexagon.Glyph.transform.position = new Vector3(0.866f * hexagon._posX - 0.433f * hexagon._posY, 0.2f, 0.75f * hexagon._posY);

            hexagon.CurrentState = Hexagon.State.Targetable;
            hexagon.Orientation = hexas[i].Orientation;

            hexas[i] = hexagon;
            
        }

        if (area.Orientation == Orientation.EnumOrientation.Line || area.Orientation == Orientation.EnumOrientation.Any)
            _finalArea = area.AreaToHexa(Direction.EnumDirection.East, hexas[1]);


        if (area.Orientation == Orientation.EnumOrientation.Diagonal)
            _finalArea = area.AreaToHexa(Direction.EnumDirection.DiagonalNorthEast, hexas[1]);


        for (int i = 0; i < _finalArea.Count; i++)
        {
            _finalArea[i].CurrentState = Hexagon.State.OverEnnemiTargetable;
            Logger.Debug(_finalArea[i]._posX + " " + _finalArea[i]._posY + " : " + _finalArea[i].CurrentState + ", " + _finalArea[i].StateChanged);
        }

        for (int i = 0; i < hexas.Count; i++)
        {
            //Logger.Debug(hexas[i].Orientation);
            if (!_finalArea.Contains(hexas[i]))
            {
                hexas[i].GameObject.SetActive(false);
            }
        }
        hexas[1].GameObject.transform.parent.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

	// Update is called once per frame
	void Update () {
	
	}

    private void LoadArea()
    {
        JSONObject js = JSONObject.GetJsonObjectFromFile("JsonFiles/Spells/area");
        JSONObject array = js.list[0];
        foreach (JSONObject area in array.list)
        {
            Area a = new Area(area);
            _areas.Add(a.getId(), a);
        }
        Logger.Debug("Area.json read");
    }

    public Area GetAreaById(int id)
    {
        Area area;
        _areas.TryGetValue(id, out area);
        return area;
    }
}
