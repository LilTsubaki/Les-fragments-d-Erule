using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Describe Spell Range
/// </summary>
class Range
{
    /// <summary>
    /// Describe how you can target with the spell
    /// </summary>


    private int _id;
    private int _minRange;
    private int _maxRange;
    private bool _piercing;
    private Orientation.EnumOrientation _orientation;
    
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
    public Range(int id, int minRange, int maxRange, bool piercing, Orientation.EnumOrientation orientation)
    {
        _id = id;
        _minRange = minRange;
        _maxRange = maxRange;
        _piercing = piercing;
        _orientation = orientation;
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
        _id = (int)js.GetField(js.keys[0]).n;
        _minRange = (int)js.GetField(js.keys[1]).n;
        _maxRange = (int)js.GetField(js.keys[2]).n;
        _piercing = js.GetField(js.keys[3]).b;
        _orientation = Orientation.stringToOrientation(js.GetField(js.keys[4]).str);
    }

    

    public int getId()
    {
        return _id;
    }
}

