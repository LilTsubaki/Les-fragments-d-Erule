using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Define a node for a spell pattern 
/// Each Node have an enumDirection and a list of childs Node 
/// </summary>
class Node
{
    List<Node> _nodes;
    Direction.EnumDirection _direction;

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

        JSONObject array = js.GetField(js.keys[1]);
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

        if(currentHexa != null)
        {
            hexas.Add(currentHexa);
        }

        for(int i = 0; i < _nodes.Count; i++)
        {
            dirs.Add(_nodes[i]._direction);
            NodeToHexagon(dirs, ref hexas, root);
        }
    }


    public void displayNodeTest()
    {
        Debug.Log("direction : " + _direction);
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

