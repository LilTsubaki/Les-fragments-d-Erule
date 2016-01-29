using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Area
{
    private int _id;
    private Orientation.EnumOrientation _orientation;
    private List<Node> _nodes;

    public Area() {}

    public Area(int id, Orientation.EnumOrientation orientation, List<Node> nodes)
    {
        _id = id;
        _orientation = orientation;
        _nodes = nodes;
    }

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

    public int getId()
    {
        return _id;
    }
}
