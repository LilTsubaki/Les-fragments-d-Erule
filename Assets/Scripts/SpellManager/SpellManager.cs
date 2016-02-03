using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpellManager
{
    private static SpellManager _SpellManager;

    private Dictionary<int, Area> _areas;
    private Dictionary<int, Range> _ranges;
    private Dictionary<uint, EffectDirect> _directEffects;
    private Dictionary<uint, EffectOnTime> _onTimeEffects;
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
        _directEffects = new Dictionary<uint, EffectDirect>();
        _onTimeEffects = new Dictionary<uint, EffectOnTime>();
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

            SelfSpell selfSpell = new SelfSpell(spell.GetField(spell.keys[1]));
            TargetSpell targetSpell = new TargetSpell(spell.GetField(spell.keys[2]));

            _elementNode.SetSelfSpell(ref selfSpell, elements);
            _elementNode.SetTargetSpell(ref targetSpell, elements);
        }

        js = JSONObject.GetJsonObjectFromFile(Application.dataPath + "/JsonFiles/directEffect.json");
        array = js.list[0];
        foreach (JSONObject directEffect in array.list)
        {
            //Logger.Error(directEffect.GetField(directEffect.keys[0]));
            Type t = Type.GetType(directEffect.GetField(directEffect.keys[0]).str);

            if (t == null)
            {
                throw new Exception("Type " + directEffect.GetField(directEffect.keys[0]).str + " not found.");
            }

            try
            {
                Type[] argTypes = new Type[] { typeof(JSONObject) };
                object[] argValues = new object[] { directEffect.GetField(directEffect.keys[1]) };
                ConstructorInfo ctor = t.GetConstructor(argTypes);
                Effect ef = (Effect)ctor.Invoke(argValues);
                _directEffects.Add(ef.GetId(), (EffectDirect)ef);
            }
            catch
            {
                throw new Exception("contructor not found.");
            }
        }

        js = JSONObject.GetJsonObjectFromFile(Application.dataPath + "/JsonFiles/onTimeEffect.json");
        array = js.list[0];
        foreach (JSONObject onTimeEffect in array.list)
        {
            Type t = Type.GetType(onTimeEffect.GetField(onTimeEffect.keys[0]).str);

            if (t == null)
            {
                throw new Exception("Type " + onTimeEffect.GetField(onTimeEffect.keys[0]).str + " not found.");
            }
            try
            {
                Type[] argTypes = new Type[] { typeof(JSONObject) };
                object[] argValues = new object[] { onTimeEffect.GetField(onTimeEffect.keys[1]) };
                ConstructorInfo ctor = t.GetConstructor(argTypes);
                Effect ef = (Effect)ctor.Invoke(argValues);
                _onTimeEffects.Add(ef.GetId(), (EffectOnTime)ef);
            }
            catch
            {
                throw new Exception("contructor not found.");
            }
            /*EffectOnTime eot = new EffectOnTime(onTimeEffect);
            _onTimeEffects.Add(eot.getId(), eot);*/
        }
    }

    public EffectDirect getDirectEffectById(uint id)
    {
        EffectDirect value;
        _directEffects.TryGetValue(id, out value);
        return value;
    }
}
