using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpellManager
{
    private static SpellManager _SpellManager;

    private Dictionary<int, Area> _areas;
    private Dictionary<int, Range> _ranges;
    private Dictionary<int, Effect> _directEffects;
   // private Dictionary<int, EffectOnTime> _onTimeEffects;
    private ElementNode _elementNode;

    private Range _CurrentRange;
    private TargetSpell _CurrentTargetSpell;
    private Area _CurrentTargetArea;
    private SelfSpell _CurrentSelfSpell;
    private Area _CurrentSelfArea;

    private Queue<Element> _spellToInit;
    private List<Element> _spellAnims;

    private Dictionary<int, Dictionary<Element, int>> _failDamage;

    public ElementNode ElementNode
    {
        get { return _elementNode; }
    }

    public TargetSpell CurrentTargetSpell
    {
        get
        {
            return _CurrentTargetSpell;
        }

        set
        {
            _CurrentTargetSpell = value;
        }
    }

    public SelfSpell CurrentSelfSpell
    {
        get
        {
            return _CurrentSelfSpell;
        }

        set
        {
            _CurrentSelfSpell = value;
        }
    }

    public Area CurrentTargetArea
    {
        get
        {
            return _CurrentTargetArea;
        }

        set
        {
            _CurrentTargetArea = value;
        }
    }

    public Area CurrentSelfArea
    {
        get
        {
            return _CurrentSelfArea;
        }

        set
        {
            _CurrentSelfArea = value;
        }
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

    private void LoadRange()
    {
        JSONObject js = JSONObject.GetJsonObjectFromFile("JsonFiles/Spells/range");
        JSONObject array = js.list[0];
        foreach (JSONObject range in array.list)
        {
            Range r = new Range(range);
            _ranges.Add(r.getId(), r);
        }
        Logger.Debug("Range.json read");
    }

    private void LoadArea()
    {
        JSONObject js = JSONObject.GetJsonObjectFromFile("JsonFiles/Spells/area");
        JSONObject array = js.list[0];
        foreach (JSONObject area in array.list)
        {
            Area a = new Area(area);
            _areas.Add(a.getId(), a);
        }
        Logger.Debug("Area.json read");
    }

    private void LoadSpell()
    {
        JSONObject js = JSONObject.GetJsonObjectFromFile("JsonFiles/Spells/spell");
        JSONObject array = js.list[0];
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

            _elementNode.SetTargetSpell(ref targetSpell, elements);

            elements = new Queue<Element>(elementsList);
            _elementNode.SetSelfSpell(ref selfSpell, elements);

        }
        Logger.Debug("spell.json read");
    }

    public void LoadDirectEffect()
    {
        JSONObject js = JSONObject.GetJsonObjectFromFile("JsonFiles/Spells/directEffect");
        JSONObject array = js.list[0];
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
            catch (Exception e)
            {
                throw e;// new Exception("contructor not found.");
            }
        }
        Logger.Debug("directEffect.json read");
    }

    public void LoadOnTimeEffect()
    {
        JSONObject js = JSONObject.GetJsonObjectFromFile("JsonFiles/Spells/onTimeEffect");
        JSONObject array = js.list[0];
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
                Logger.Warning(ef.GetId());
                _directEffects.Add(ef.GetId(), (EffectOnTime)ef);
            }
            catch(Exception e)
            {
                throw e;// new Exception(onTimeEffect.GetField(onTimeEffect.keys[0]).str+" contructor not found.");
            }
        }
        Logger.Debug("onTimeEffect.json read");
    }

    public void LoadFailDamage()
    {
        JSONObject js = JSONObject.GetJsonObjectFromFile("JsonFiles/Spells/failDamage");
        JSONObject array = js.list[0];
        _failDamage = new Dictionary<int, Dictionary<Element, int>>();
        foreach (JSONObject failDamage in array.list)
        {
            int nbRunes = (int)(failDamage.GetField("nbRunes").f);
            Dictionary<Element, int> damages = new Dictionary<Element, int>();
            foreach (JSONObject obj in failDamage.GetField("damages").list)
            {
                Element elem = Element.GetElement((int)(obj.GetField("element").f));
                damages.Add(elem, (int)(obj.GetField("damage").f));

            }
            _failDamage.Add(nbRunes, damages);
        }
        Logger.Debug("failDamage.json read");
    }

    /// <summary>
    /// Read json files to fill dictionaries
    /// </summary>
    private void init()
    {
        _ranges = new Dictionary<int, Range>();
        _areas = new Dictionary<int, Area>();
        _directEffects = new Dictionary<int, Effect>();
        //_onTimeEffects = new Dictionary<int, EffectOnTime>();
        _elementNode = ElementNode.GetInstance();

        LoadRange();

        LoadArea();

        LoadSpell();

        LoadDirectEffect();

        LoadOnTimeEffect();

        LoadFailDamage();
    }

    public void SetSpellToInit(Queue<Element> queue)
    {
        _spellToInit = queue;
    }

    public Effect GetDirectEffectById(int id)
    {
        Effect value;
        _directEffects.TryGetValue(id, out value);
        return value;
    }

    public Range GetRangeById(int id)
    {
        Range range;
        _ranges.TryGetValue(id, out range);
        return range;
    }

    public Area GetAreaById(int id)
    {
        Area area;
        _areas.TryGetValue(id, out area);
        return area;
    }

    public void InitRange()
    {
        List<Hexagon> ranges= PlayBoardManager.GetInstance().Board.GetRange(_CurrentRange, PlayBoardManager.GetInstance().GetCurrentPlayer().Position);
        //Logger.Trace("taille list range : " + range.Count);
        
        for (int i = 0; i < ranges.Count; i++)
        {
            if (ranges[i].GameObject != null)
            {
                if(_CurrentRange.Piercing && ranges[i].isVisible())
                {
                    if(_CurrentRange.EnemyTargetable || (ranges[i]._entity==null && ranges[i].Portal == null) || ranges[i]._entity== PlayBoardManager.GetInstance().GetCurrentPlayer() || !(ranges[i]._entity is Killable))
                    ranges[i].CurrentState = Hexagon.State.Targetable;
                }
                else
                {
                    if(PlayBoardManager.GetInstance().Board.fieldOfView(PlayBoardManager.GetInstance().GetCurrentPlayer().Position, ranges[i]) && ranges[i].isVisible())
                    {
                        if (_CurrentRange.EnemyTargetable || (ranges[i]._entity == null && ranges[i].Portal == null) || ranges[i]._entity == PlayBoardManager.GetInstance().GetCurrentPlayer() || !(ranges[i]._entity is Killable))
                            ranges[i].CurrentState = Hexagon.State.Targetable;
                    }
                }
            }
        }
    }

    public void InitSpell()
    {
        if(_spellToInit != null)
        {
            PlayBoardManager.GetInstance().Board.ResetBoard();
            InitSpell(_spellToInit);
            _spellToInit = null;
        }
    }

    public void InitSpell(Queue<Element> elements)
    {
        Queue<Element> tempElements = new Queue<Element>(elements);
        _spellAnims = new List<Element>(tempElements.ToArray());

        CurrentTargetSpell = ElementNode.GetTargetSpell(elements);
        CurrentTargetArea = GetAreaById(CurrentTargetSpell.AreaId);

        CurrentSelfSpell = ElementNode.GetSelfSpell(tempElements);
        CurrentSelfArea = GetAreaById(CurrentSelfSpell.AreaId);

        _CurrentRange = GetRangeById(CurrentTargetSpell._rangeId);
        InitRange();       
    }

    public void ApplyEffects(List<Hexagon> finalArea, Hexagon target)
    {
        Character currentPlayer = PlayBoardManager.GetInstance().GetCurrentPlayer();

        float perfection, sublimation, stability;
        RunicBoardManager.GetInstance().GetPolesInfluence(out perfection, out sublimation, out stability);

        bool success = ProcessStability(stability);
        bool crit = ProcessSublimation(sublimation);
        int runes = ProcessPerfection(perfection);

        List<int> effectIds;
        

        if (target == currentPlayer.Position )
        {
            Logger.Trace("apply selfEffects");
            effectIds = CurrentSelfSpell.EffectsArea.GetIds();
            if (effectIds.Count != 0)
            {
                for (int i = 0; i < effectIds.Count; i++)
                {
                    Effect effectTest = SpellManager.getInstance().GetDirectEffectById((int)effectIds[i]);
                    if (effectTest != null)
                        effectTest.ApplyEffect(finalArea, target, currentPlayer);
                }
            }

            effectIds = CurrentSelfSpell._effects.GetIds();
            if (effectIds.Count != 0)
            {
                for (int i = 0; i < effectIds.Count; i++)
                {
                    Effect effectTest = SpellManager.getInstance().GetDirectEffectById((int)effectIds[i]);
                    if (effectTest != null)
                        effectTest.ApplyEffect(finalArea, target, currentPlayer);
                }
            }

            // If the spell is critically casted, we add the critical effects
            if (crit)
            {
                effectIds = CurrentSelfSpell.EffectsAreaCrit.GetIds();
                if (effectIds.Count != 0)
                {
                    for (int i = 0; i < effectIds.Count; i++)
                    {
                        Effect effectTest = SpellManager.getInstance().GetDirectEffectById((int)effectIds[i]);
                        if (effectTest != null)
                            effectTest.ApplyEffect(finalArea, target, currentPlayer);
                    }
                }

                effectIds = CurrentSelfSpell._effectsCrit.GetIds();
                if(effectIds.Count != 0)
                {
                    for (int i = 0; i < effectIds.Count; i++)
                    {
                        Effect effectTest = SpellManager.getInstance().GetDirectEffectById((int)effectIds[i]);
                        if (effectTest != null)
                            effectTest.ApplyEffect(finalArea, target, currentPlayer);
                    }
                }
            }
        }
        else
        {
            /*********************
            À modifier dès qu'on a différents effets visuels
            **********************/

            SpellAnimationManager.GetInstance().PlayList(_spellAnims, currentPlayer._gameObject, target.GameObject);
            /*SpellAnimationManager.GetInstance().Play("air", currentPlayer._gameObject, target.GameObject);
            SpellAnimationManager.GetInstance().Play("metal1", currentPlayer._gameObject, target.GameObject);*/

            /*********************

            *********************/


            Logger.Trace("apply targetEffects");
            effectIds = CurrentTargetSpell.EffectsArea.GetIds();
            if (effectIds.Count != 0)
            {
                for (int i = 0; i < effectIds.Count; i++)
                {
                    Effect effectTest = SpellManager.getInstance().GetDirectEffectById((int)effectIds[i]);
                    if (effectTest != null)
                        effectTest.ApplyEffect(finalArea, target, currentPlayer);
                }
            }
            // If the spell is critically casted, we add the critical effects
            if (crit)
            {
                effectIds = CurrentTargetSpell.EffectsAreaCrit.GetIds();
                if(effectIds.Count != 0)
                {
                    for (int i = 0; i < effectIds.Count; i++)
                    {
                        Effect effectTest = SpellManager.getInstance().GetDirectEffectById((int)effectIds[i]);
                        if (effectTest != null)
                            effectTest.ApplyEffect(finalArea, target, currentPlayer);
                    }
                }
            }
        }


        ServerManager.GetInstance()._server.ApplyEffects(currentPlayer, success, crit, runes);

    }

    /// <summary>
    /// Processes the effects of the perfection pole. It can prevent some runes from returning into the hand.
    /// </summary>
    /// <param name="perfection">The percentage of chance to keep runes due to the perfection pole.</param>
    /// <returns>The number of rune to keep.</returns>
    public int ProcessPerfection(float perfection)
    {
        bool ignoreSecond = true;
        float keepCentral = UnityEngine.Random.value;
        float keepSecond = 0;

        if(keepCentral < perfection)
        {
            if(perfection >= 0.2f)
            {
                keepSecond = UnityEngine.Random.value;
                if(keepSecond < perfection)
                {
                    ignoreSecond = false;
                }
                if(ignoreSecond)
                {
                    return 1;
                }
                return 2;
            }
        }
        return 0;
    }

    /// <summary>
    /// Processes the effects of the sublimation pole. It can inflict more effects from a spell.
    /// </summary>
    /// <param name="sublimation">The percentage of chance to add effects to a spell due to the sublimation pole.</param>
    /// <returns>True if effects are to be added. Else false.</returns>
    public bool ProcessSublimation(float sublimation)
    {
        return sublimation >= UnityEngine.Random.value;
    }

    /// <summary>
    /// Processes the effects of the stability pole on the chances to cast a spell successfully. 
    /// </summary>
    /// <param name="stability">The percentage of chance to cast successfully to add to the compatibility between runes.</param>
    /// <returns>True if the cast succeded. Else false.</returns>
    public bool ProcessStability(float stability)
    {
        float baseChance = RunicBoardManager.GetInstance().GetBaseStabilityByRuneNumber();
        float coef = RunicBoardManager.GetInstance().GetReductionCoefficientByRuneNumber();
        float malus = RunicBoardManager.GetInstance().GetTotalCompatibilityMalus();
        float totalStability = (baseChance - (malus / coef)) * 0.01f + stability;

        return totalStability >= UnityEngine.Random.value;
    }

}
