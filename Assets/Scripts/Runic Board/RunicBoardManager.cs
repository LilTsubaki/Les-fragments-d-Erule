using UnityEngine;
using System.Collections.Generic;

public class RunicBoardManager{

    static private RunicBoardManager _instance;

    private RunicBoard _boardPlayer1;
    //private RunicBoard _boardPlayer2;

    private Dictionary<int, CompatibilityMalus> _compatibilityMaluses;
    private Dictionary<string, Compatibility> _compatibilities;

    RunicBoardManager()
    {
        _compatibilityMaluses = new Dictionary<int, CompatibilityMalus>();
        _compatibilities = new Dictionary<string, Compatibility>();
    }

    public static RunicBoardManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new RunicBoardManager();
        }
        return _instance;
    }

    public void RegisterBoard(RunicBoard board)
    {
        _boardPlayer1 = board;
    }

    public RunicBoard GetBoardPlayer1()
    {
        return _boardPlayer1;
    }

    /*public RunicBoard GetBoardPlayer2()
    {
        return _boardPlayer2;
    }*/

    private void Init()
    {
        JSONObject js = JSONObject.GetJsonObjectFromFile(Application.dataPath + "/JsonFiles/compatibilityMalus.json");
        JSONObject array = js.list[0];
        foreach(JSONObject malus in array.list)
        {
            CompatibilityMalus m = new CompatibilityMalus(malus);
            _compatibilityMaluses.Add(m.Id, m);
        }

        js = JSONObject.GetJsonObjectFromFile(Application.dataPath + "/JsonFiles/compatibility.json");
        array = js.list[0];
        foreach(JSONObject comp in array.list)
        {
            string id = "";
            foreach(JSONObject elem in comp.GetField(comp.keys[0]).list)
            {
                id += elem.n;
            }
            int idMalus = (int) comp.GetField(comp.keys[1]).n;
            CompatibilityMalus mal;
            _compatibilityMaluses.TryGetValue(idMalus, out mal);
            Compatibility compa = new Compatibility(id, mal);
            _compatibilities.Add(id, compa);
        }
    }
}
