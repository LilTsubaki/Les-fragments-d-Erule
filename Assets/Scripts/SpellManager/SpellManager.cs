using System.Collections.Generic;
using UnityEngine;

public class SpellManager
{
    private static SpellManager _SpellManager;

    private Dictionary<int, Area> _areas;
    private Dictionary<int, Range> _ranges;
    private Dictionary<int, EffectDirect> _directEffects;
    private ElementNode _elementNode;

    public ElementNode ElementNode
    {
        get { return _elementNode; }
    }

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
        _directEffects = new Dictionary<int, EffectDirect>();
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

        js = JSONObject.GetJsonObjectFromFile(Application.dataPath + "/JsonFiles/spell.json");
        array = js.list[0];
        foreach (JSONObject spell in array.list)
        {
            List<Element> elementsList = new List<Element>();
            Queue<Element> elements;
            JSONObject runeArray = spell.GetField(spell.keys[0]);
            foreach (JSONObject rune in runeArray.list)
            {
                elementsList.Add(Element.GetElement((int)rune.n));
                Logger.Trace((int)rune.n);
            }
            elementsList.Sort();
            elements = new Queue<Element>(elementsList);

            SelfSpell selfSpell= new SelfSpell(spell.GetField(spell.keys[1]));
            TargetSpell targetSpell = new TargetSpell(spell.GetField(spell.keys[2]));

            _elementNode.SetSelfSpell(ref selfSpell, elements);
            _elementNode.SetTargetSpell(ref targetSpell, elements);
        }
    }
}
