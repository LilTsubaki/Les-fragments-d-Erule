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

    public Node()
    {
        Nodes = new List<Node>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="direction"></param>
    public Node(Direction.EnumDirection direction, bool nodeUsed,List<Node> nodes)
    {
        Nodes = nodes;
        _direction = direction;
        _nodeUsed = nodeUsed;
    }

    /// <summary>
    /// Constructor from jsonobject
    /// </summary>
    /// <param name="js"></param>
    public Node(JSONObject js)
    {
        Nodes = new List<Node>();

        //Debug.Log(Direction.stringToDirection(js.GetField(js.keys[0]).str));
        _direction = Direction.stringToDirection(js.GetField(js.keys[0]).str);
        _nodeUsed =  js.GetField(js.keys[1]).b;
        JSONObject array = js.GetField(js.keys[2]);
        //Debug.Log("nb sous node : " + array.Count);
        foreach (JSONObject node in array.list)
        {
            Node n = new Node(node);
            Nodes.Add(n);
        }
    }

    public Node rotateNode(int rotateValue)
    {
        Node n = new Node();
        n._nodeUsed = _nodeUsed;
        n._direction = Direction.Rotate(_direction, rotateValue);
        for(int i = 0; i < Nodes.Count; i++)
        {
            n.Nodes.Add(Nodes[i].rotateNode(rotateValue));
        }
        return n;
    }

    public void NodeToHexagon(List<Direction.EnumDirection> dirs, ref List<Hexagon> hexas, Hexagon root)
    {
        Hexagon currentHexa;
        currentHexa = root.GetTarget(dirs);

        //Logger.Error("posx : " + currentHexa._posX + "posy : " + currentHexa._posY + "used : " +NodeUsed);
        if (currentHexa != null && currentHexa._posX >=0 && currentHexa._posY >= 0 && NodeUsed && currentHexa.isVisible())
        {
            hexas.Add(currentHexa);
            //Logger.Error("size ---------------->" + hexas.Count);
        }

        if ((currentHexa._posX == -1 && currentHexa._posY == -1) || currentHexa.isVisible()) 
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                dirs.Add(Nodes[i]._direction);
                Nodes[i].NodeToHexagon(dirs, ref hexas, root);
                dirs.RemoveAt(dirs.Count - 1);
            }
        }
        
    }


    public void displayNodeTest()
    {
        //Debug.Log("direction : " + _direction);
        for(int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].displayNodeTest();
        }
    }

    public Direction.EnumDirection getDirection()
    {
        return _direction;
    }

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

