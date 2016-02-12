using UnityEngine;
using System.Collections;

public class RuneNumberInfluence {

    private int _number;
    private float _baseStability;
    private float _reductionCoefficient;

    public int Number
    {
        get
        {
            return _number;
        }

        set
        {
            _number = value;
        }
    }

    public float BaseStability
    {
        get
        {
            return _baseStability;
        }

        set
        {
            _baseStability = value;
        }
    }

    public float ReductionCoefficient
    {
        get
        {
            return _reductionCoefficient;
        }

        set
        {
            _reductionCoefficient = value;
        }
    }

    public RuneNumberInfluence() { }

    public RuneNumberInfluence(int number, float maxStability, float diminishingCoefficient)
    {
        Number = number;
        BaseStability = maxStability;
        ReductionCoefficient = diminishingCoefficient;
    }

    public RuneNumberInfluence(JSONObject js)
    {
        Number = (int) js.GetField(js.keys[0]).n;
        BaseStability = js.GetField(js.keys[1]).n;
        ReductionCoefficient = js.GetField(js.keys[2]).n;
    }

}
