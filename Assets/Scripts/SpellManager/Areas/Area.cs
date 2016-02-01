using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Define a spell pattern
/// </summary>
class Area
{
    private int _id;
    private Orientation.EnumOrientation _orientation;
    private List<Node> _nodes;

    public Area()
    {
        _nodes = new List<Node>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="orientation"></param>
    /// <param name="nodes"></param>
    public Area(int id, Orientation.EnumOrientation orientation, List<Node> nodes)
    {
        _id = id;
        _orientation = orientation;
        _nodes = nodes;
    }

    /// <summary>
    /// Constructor from jsonobject
    /// </summary>
    /// <param name="js"></param>
    public Area(JSONObject js)
    {
        /*Debug.Log(js.ToString());
        Debug.Log((int)js.GetField(js.keys[0]).n);
        Debug.Log(Orientation.stringToOrientation(js.GetField(js.keys[1]).str));*/

        _nodes = new List<Node>();
        _id = (int)js.GetField(js.keys[0]).n;
        _orientation = Orientation.stringToOrientation(js.GetField(js.keys[1]).str);
        JSONObject array = js.GetField(js.keys[2]);
        //Debug.Log(array.Count);

        foreach (JSONObject node in array.list)
        {
            Node n = new Node(node);
            _nodes.Add(n);
        }

    }

    public Area rotateArea(Direction.EnumDirection cible)
    {
        List<Node> nodes = new List<Node>();
        int rotateValue = 0;

        if (_orientation == Orientation.EnumOrientation.Line)
        {
            rotateValue = Direction.GetDiff(Direction.EnumDirection.East, cible);            
        }

        if (_orientation == Orientation.EnumOrientation.Diagonal)
        {
            rotateValue = Direction.GetDiff(Direction.EnumDirection.DiagonalSouthEast, cible);
        }

        if (rotateValue % 2 == 0)
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                nodes.Add(_nodes[i].rotateNode(rotateValue));
            }
        }

        return new Area(_id, _orientation, nodes);
    }

    public int getId()
    {
        return _id;
    }

    /// <summary>
    /// return an hexagon list that macthes the area 
    /// </summary>
    /// <param name="direction">Direction between target and player</param>
    /// <param name="hexa">target</param>
    /// <returns></returns>
    public List<Hexagon> AreaToHexa(Direction.EnumDirection direction, Hexagon source)
    {
        List<Hexagon> turnedArea = new List<Hexagon>();
        turnedArea.Add(source);
        //rotate the area using directiion as target
        Area area = rotateArea(direction);

        if(area._nodes.Count !=0)
        {
            for(int i = 0; i < area._nodes.Count; i++)
            {
                List<Direction.EnumDirection> dirs = new List<Direction.EnumDirection>();
                dirs.Add(area._nodes[i].getDirection());
                area._nodes[i].NodeToHexagon(dirs, ref turnedArea, source);
            }
        }
        return turnedArea;
    }

    public void displayAreaTest()
    {
        Debug.Log("id area : " + _id);
        Debug.Log("orientation : " + _orientation);
        for(int i = 0; i < _nodes.Count; i++)
        {
            _nodes[i].displayNodeTest();
        }
    }
}
