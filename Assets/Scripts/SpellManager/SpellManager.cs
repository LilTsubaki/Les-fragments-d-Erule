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

        JSONObject js = JSONObject.GetJsonObjectFromFile(Application.dataPath + "/JsonFiles/range.json");
        JSONObject array = js.list[0];
        foreach (JSONObject range in array.list)
        {
            Range r = new Range(range);
            _ranges.Add(r.getId(), r);
        }
        //Debug.Log(_ranges.Count);

        js = JSONObject.GetJsonObjectFromFile(Application.dataPath + "/JsonFiles/area.json");
        array = js.list[0];
        foreach (JSONObject area in array.list)
        {
            Area a = new Area(area);
            _areas.Add(a.getId(), a);
        }

        // TEST FONCTION AREATOHEXA
        /*PlayBoard board = new PlayBoard(10, 10);
        Hexagon hex1 = board.CreateHexagone(0, 0);
        Hexagon hex2 = board.CreateHexagone(1, 0);
        Hexagon hex3 = board.CreateHexagone(2, 0);
        Hexagon hex4 = board.CreateHexagone(0, 1);
        Hexagon hex5 = board.CreateHexagone(1, 1);
        Hexagon hex6 = board.CreateHexagone(2, 1);
        Hexagon hex7 = board.CreateHexagone(0, 2);
        Hexagon hex8 = board.CreateHexagone(1, 2);
        Hexagon hex9 = board.CreateHexagone(2, 2);

        List<Hexagon> hexas = _areas[1].AreaToHexa(Direction.EnumDirection.East, hex4);
        foreach(Hexagon hex in hexas)
        {
            Logger.Error(hex._posX + " " + hex._posY);
        }*/

        //_areas[1].rotateArea(Direction.EnumDirection.SouthWest).displayAreaTest();

    }
}
