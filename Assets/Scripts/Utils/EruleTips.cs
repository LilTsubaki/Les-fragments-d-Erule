using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EruleTips {

    private static EruleTips _instance;
    private List<string> tips;

    public static EruleTips GetInstance()
    {
        if (_instance == null)
        {
            _instance = new EruleTips();
            _instance.Init();
        }
        return _instance;
    }

    private void Init()
    {
        tips = new List<string>();
        JSONObject js = JSONObject.GetJsonObjectFromFile("JsonFiles/tips");
        JSONObject array = js.GetField("tips");
        foreach(JSONObject str in array.list)
        {
            tips.Add(str.str);
        }
    }

    public string GetRandomTip()
    {
        int rand = EruleRandom.RangeValue(0, tips.Count - 1);
        return tips[rand];
    }
}
