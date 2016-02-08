using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// define Spell Range
/// </summary>
public class Range
{
    private uint _id;
    private int _minRange;
    private int _maxRange;
    private bool _piercing;
    private Orientation.EnumOrientation _orientation;

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

    /// <summary>
    /// Default constructor
    /// </summary>
    public Range() { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="minRange"></param>
    /// <param name="maxRange"></param>
    /// <param name="piercing"></param>
    /// <param name="orientation"></param>
    public Range(uint id, int minRange, int maxRange, bool piercing, Orientation.EnumOrientation orientation)
    {
        _id = id;
        _minRange = minRange;
        _maxRange = maxRange;
        _piercing = piercing;
        Orientation = orientation;
    }
    /// <summary>
    /// Constructor from jsonobject
    /// </summary>
    /// <param name="js"></param>
    public Range(JSONObject js)
    {
        /*Debug.Log(js.ToString());
        Debug.Log((int)js.GetField(js.keys[0]).n);
        Debug.Log((int)js.GetField(js.keys[1]).n);
        Debug.Log((int)js.GetField(js.keys[2]).n);
        Debug.Log(js.GetField(js.keys[3]).b);
        Debug.Log(Orientation.stringToOrientation(js.GetField(js.keys[4]).str));*/
        _id = (uint)js.GetField(js.keys[0]).n;
        _minRange = (int)js.GetField(js.keys[1]).n;
        _maxRange = (int)js.GetField(js.keys[2]).n;
        _piercing = js.GetField(js.keys[3]).b;
        Orientation = global::Orientation.stringToOrientation(js.GetField(js.keys[4]).str);
    }

    

    public uint getId()
    {
        return _id;
    }
}

