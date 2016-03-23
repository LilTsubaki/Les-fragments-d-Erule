using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Define a spell pattern
/// </summary>
public class Area
{
    private int _id;
    private Orientation.EnumOrientation _orientation;
    private List<Node> _nodes;
    private bool _rootUsed;



    public Area()
    {
        Nodes = new List<Node>();
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
        Orientation = orientation;
        Nodes = nodes;
    }

    public Area(Orientation.EnumOrientation orientation, bool rootUsed, List<Node> nodes)
    {
        _id = 0;
        RootUsed = rootUsed;
        Orientation = orientation;
        Nodes = nodes;
    }

    /// <summary>
    /// Constructor from jsonobject
    /// </summary>
    /// <param name="js"></param>
    public Area(JSONObject js)
    {
        Nodes = new List<Node>();
        _id = (int)js.GetField(js.keys[0]).n;
        Orientation = global::Orientation.stringToOrientation(js.GetField(js.keys[1]).str);
        RootUsed = js.GetField(js.keys[2]).b;
        //Logger.Error("rootused ---------> : " + _rootUsed);

        JSONObject array = js.GetField(js.keys[3]);

        foreach (JSONObject node in array.list)
        {
            Node n = new Node(node);
            Nodes.Add(n);
        }

    }

    public Area rotateArea(Direction.EnumDirection cible)
    {
        List<Node> nodes = new List<Node>();
        int rotateValue = 0;

        if (Orientation == global::Orientation.EnumOrientation.Line)
        {
            rotateValue = Direction.GetDiff(Direction.EnumDirection.East, cible);            
        }

        if (Orientation == global::Orientation.EnumOrientation.Diagonal)
        {
            rotateValue = Direction.GetDiff(Direction.EnumDirection.DiagonalSouthEast, cible);
        }

        if (rotateValue % 2 == 0)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                nodes.Add(Nodes[i].rotateNode(rotateValue));
            }
        }

        return new Area(_id, Orientation, nodes);
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
        if(RootUsed)
            turnedArea.Add(source);
        //rotate the area using direction as target
        Area area;
        if (direction != Direction.EnumDirection.Default)
            area = rotateArea(direction);
        else
            area = this;

        if (area.Nodes.Count !=0)
        {
            for(int i = 0; i < area.Nodes.Count; i++)
            {
                List<Direction.EnumDirection> dirs = new List<Direction.EnumDirection>();
                dirs.Add(area.Nodes[i].getDirection());
                //Logger.Error("lalalalalalalal : " + area._nodes[i].NodeUsed);
                area.Nodes[i].NodeToHexagon(dirs, ref turnedArea, source);
            }
        }
        //Logger.Error("turnedArea" + turnedArea.Count);
        return turnedArea;
    }

    public void displayAreaTest()
    {
        Debug.Log("id area : " + _id);
        Debug.Log("orientation : " + Orientation);
        for(int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].displayNodeTest();
        }
    }

    public Orientation.EnumOrientation Orientation
    {
        get
        {
            return _orientation;
        }

        set
        {
            _orientation = value;
        }
    }

    public bool RootUsed
    {
        get
        {
            return _rootUsed;
        }

        set
        {
            _rootUsed = value;
        }
    }

    public List<Node> Nodes
    {
        get
        {
            return _nodes;
        }

        set
        {
            _nodes = value;
        }
    }
}
