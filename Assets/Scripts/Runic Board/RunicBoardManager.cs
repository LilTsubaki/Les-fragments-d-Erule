using UnityEngine;
using System.Collections.Generic;

public class RunicBoardManager{

    static private RunicBoardManager _instance;

    private RunicBoard _boardPlayer1;
    //private RunicBoard _boardPlayer2;

    private Dictionary<int, CompatibilityMalus> _compatibilityMaluses;
    private Dictionary<string, Compatibility> _compatibilities;
    private Dictionary<int, RuneNumberInfluence> _runeNumberInfluences;

    RunicBoardManager()
    {
        _compatibilityMaluses = new Dictionary<int, CompatibilityMalus>();
        _compatibilities = new Dictionary<string, Compatibility>();
        _runeNumberInfluences = new Dictionary<int, RuneNumberInfluence>();
        Init();
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

        js = JSONObject.GetJsonObjectFromFile(Application.dataPath + "/JsonFiles/runeNumberInfluence.json");
        array = js.list[0];
        foreach(JSONObject influ in array.list)
        {
            RuneNumberInfluence influence = new RuneNumberInfluence(influ);
            _runeNumberInfluences.Add(influence.Number, influence);
        }
    }

    public float GetCompatibility(Element elem1, Element elem2)
    {
        string idCompa;
        if(elem1.CompareTo(elem2) > 0)
        {
            idCompa = elem2._id + "" + elem1._id;
        }
        else
        {
            idCompa = elem1._id + "" + elem2._id;
        }
        Compatibility c;
        _compatibilities.TryGetValue(idCompa, out c);
        if(c != null)
        {
            Logger.Debug("Compatibility malus : " + idCompa + " / " + c.GetCompatibilityMalus());
            return c.GetCompatibilityMalus();
        }
        Logger.Error("Unkown compatibility id : " + idCompa + ". Malus set to 0.");
        return 0;
    }

    public float GetTotalCompatibilityMalus()
    {
        List<KeyValuePair<Element, Element>> listLink = _boardPlayer1.GetRuneLinks();
        float totalMalus = 0;
        foreach(KeyValuePair<Element, Element> link in listLink)
        {
            totalMalus += GetCompatibility(link.Key, link.Value);
        }

        return totalMalus;
    }

    public float GetBaseStabilityByRuneNumber()
    {
        int nb = _boardPlayer1.RunesOnBoard.Count;
        return _runeNumberInfluences[nb].BaseStability;
    }

    public float GetReductionCoefficientByRuneNumber()
    {
        int nb = _boardPlayer1.RunesOnBoard.Count;
        return _runeNumberInfluences[nb].ReductionCoefficient;
    }


}
