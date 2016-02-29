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
    private int _id;
    private int _minRange;
    private int _maxRange;
    private bool _piercing;
    private bool _ennemyTargetable;
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

    public int MaxRange
    {
        get
        {
            return _maxRange;
        }

        set
        {
            _maxRange = value;
        }
    }

    public int MinRange
    {
        get
        {
            return _minRange;
        }

        set
        {
            _minRange = value;
        }
    }

    public bool Piercing
    {
        get
        {
            return _piercing;
        }

        set
        {
            _piercing = value;
        }
    }

    public bool EnnemyTargetable
    {
        get
        {
            return _ennemyTargetable;
        }

        set
        {
            _ennemyTargetable = value;
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
    public Range(int id, int minRange, int maxRange, bool piercing, Orientation.EnumOrientation orientation)
    {
        _id = id;
        MinRange = minRange;
        MaxRange = maxRange;
        Piercing = piercing;
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
        _id = (int)js.GetField(js.keys[0]).n;
        MinRange = (int)js.GetField(js.keys[1]).n;
        MaxRange = (int)js.GetField(js.keys[2]).n;
        Piercing = js.GetField(js.keys[3]).b;
        Orientation = global::Orientation.stringToOrientation(js.GetField(js.keys[4]).str);
        if(js.GetField("ennemyTargetable") != null)
        {
            EnnemyTargetable = js.GetField("ennemyTargetable").b;
        }
        else
        {
            EnnemyTargetable = true;
        }
    }

    

    public int getId()
    {
        return _id;
    }
}

