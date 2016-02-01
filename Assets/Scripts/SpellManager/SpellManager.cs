using System.Collections.Generic;
using UnityEngine;

public class SpellManager
{
    private static SpellManager _SpellManager;

    private Dictionary<int, Area> _areas;
    private Dictionary<int, Range> _ranges;
    private Dictionary<int, Effect> _effects;
    private ElementNode _elementNode;

    private SpellManager() { }

    /// <summary>
    /// spellManager singleton getInstance()
    /// </summary>
    /// <returns>unique instance of spellManager</returns>
    public static SpellManager getInstance()
    {
        if (_SpellManager == null)
        {
            _SpellManager = new SpellManager();
            _SpellManager.init();
        }
            

        return _SpellManager;
    }
   
    /// <summary>
    /// Read json files to fill dictionaries
    /// </summary>
    private void init()
    {
        _ranges = new Dictionary<int, Range>();
        _areas = new Dictionary<int, Area>();
        _effects = new Dictionary<int, Effect>();
        _elementNode = ElementNode.GetInstance();

        JSONObject js = JSONObject.GetJsonObjectFromFile(Application.dataPath + "/JsonFiles/range.json");
        JSONObject array = js.list[0];
        foreach (JSONObject range in array.list)
        {
            Range r = new Range(range);
            _ranges.Add(r.getId(), r);
        }

        js = JSONObject.GetJsonObjectFromFile(Application.dataPath + "/JsonFiles/area.json");
        array = js.list[0];
        foreach (JSONObject area in array.list)
        {
            Area a = new Area(area);
            _areas.Add(a.getId(), a);
        }
    }
}
