using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Define a node for a spell pattern 
/// Each Node have an enumDirection and a list of childs Node 
/// </summary>
public class Node
{
    private List<Node> _nodes;
    private Direction.EnumDirection _direction;
    private bool _nodeUsed;

    public bool NodeUsed
    {
        get
        {
            return _nodeUsed;
        }

        set
        {
            _nodeUsed = value;
        }
    }

    public Node()
    {
        _nodes = new List<Node>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="direction"></param>
    public Node(List<Node> nodes, Direction.EnumDirection direction)
    {
        _nodes = nodes;
        _direction = direction;
    }

    /// <summary>
    /// Constructor from jsonobject
    /// </summary>
    /// <param name="js"></param>
    public Node(JSONObject js)
    {
        _nodes = new List<Node>();

        //Debug.Log(Direction.stringToDirection(js.GetField(js.keys[0]).str));
        _direction = Direction.stringToDirection(js.GetField(js.keys[0]).str);
        _nodeUsed =  js.GetField(js.keys[1]).b;
        JSONObject array = js.GetField(js.keys[2]);
        //Debug.Log("nb sous node : " + array.Count);
        foreach (JSONObject node in array.list)
        {
            Node n = new Node(node);
            _nodes.Add(n);
        }
    }

    public Node rotateNode(int rotateValue)
    {
        Node n = new Node();
        n._nodeUsed = _nodeUsed;
        n._direction = Direction.Rotate(_direction, rotateValue);
        for(int i = 0; i < _nodes.Count; i++)
        {
            n._nodes.Add(_nodes[i].rotateNode(rotateValue));
        }
        return n;
    }

    public void NodeToHexagon(List<Direction.EnumDirection> dirs, ref List<Hexagon> hexas, Hexagon root)
    {
        Hexagon currentHexa;
        currentHexa = root.GetTarget(dirs);

        //Logger.Error("posx : " + currentHexa._posX + "posy : " + currentHexa._posY + "used : " +NodeUsed);
        if (currentHexa != null && currentHexa._posX >=0 && currentHexa._posY >= 0 && NodeUsed)
        {
            hexas.Add(currentHexa);
            //Logger.Error("size ---------------->" + hexas.Count);
        }

        for(int i = 0; i < _nodes.Count; i++)
        {
            dirs.Add(_nodes[i]._direction);
            _nodes[i].NodeToHexagon(dirs, ref hexas, root);
            dirs.RemoveAt(dirs.Count - 1);
        }
    }


    public void displayNodeTest()
    {
        //Debug.Log("direction : " + _direction);
        for(int i = 0; i < _nodes.Count; i++)
        {
            _nodes[i].displayNodeTest();
        }
    }

    public Direction.EnumDirection getDirection()
    {
        return _direction;
    }
}

