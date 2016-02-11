using UnityEngine;
using System.Collections;

public class CompatibilityMalus {

    private int _id;
    private float _power;

    public int Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    public float Power
    {
        get
        {
            return _power;
        }

        set
        {
            _power = value;
        }
    }

    public CompatibilityMalus() { }

    public CompatibilityMalus(int id, int power)
    {
        _id = id;
        _power = power;
    }

    public CompatibilityMalus(JSONObject js)
    {
        _id = (int) js.GetField(js.keys[0]).n;
        _power = js.GetField(js.keys[1]).n;
    }
}
