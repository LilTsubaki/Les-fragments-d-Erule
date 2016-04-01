using UnityEngine;
using System.Collections.Generic;

public class SpellAnimationManager {

    static private SpellAnimationManager _instance;
    private Dictionary<string, SpellAnimation> _animations;
    private List<Element> _elementsHierarchy;

    private SpellAnimationManager()
    {
        _animations = new Dictionary<string, SpellAnimation>();
        _elementsHierarchy = new List<Element>();
        _elementsHierarchy.Add(Element.GetElement(1));
        _elementsHierarchy.Add(Element.GetElement(4));
        _elementsHierarchy.Add(Element.GetElement(2));
        _elementsHierarchy.Add(Element.GetElement(5));
        _elementsHierarchy.Add(Element.GetElement(3));
        _elementsHierarchy.Add(Element.GetElement(0));
    }

    public static SpellAnimationManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new SpellAnimationManager();
        }

        return _instance;
    }

    public bool Register(string id, SpellAnimation anim)
    {
        if (_animations.ContainsKey(id))
            return false;

        _animations.Add(id, anim);
        return true;
    }

    public bool Play(string id, Vector3 from, Vector3 to)
    {
        SpellAnimation anim;
        if(_animations.TryGetValue(id, out anim))
        {
            anim.Reset(from, to);
            anim.Play();
            return true;
        }
        return false;
    }

    public bool PlayList(List<Element> elemIds, Vector3 from, Vector3 to)
    {
        HashSet<Element> setElems = new HashSet<Element>(elemIds.ToArray());
        int nbMetal = elemIds.FindAll(delegate (Element e) { return e._id == 5; }).Count;//EruleRandom.RangeValue(1, 4);
        foreach (Element elem in setElems)
        {
            switch (elem._id)
            {
                case 0: // Fire
                    Play("fire", from, to);
                    break;
                case 1: // Water
					Play("water",from,to);
                    break;
                case 2: // Air
                    Play("air", from, to);
                    break;
                case 3: // Earth
                    break;
                case 4: // Wood
                    Play("wood", from, to);
                    break;
                case 5: // Metal
                    List<int> metals = new List<int>();
                    for(int i = 0; i < Mathf.Min(nbMetal, 3); ++i)
                    {
                        int randAnimMetal = EruleRandom.RangeValue(1, 4);
                        while (metals.Contains(randAnimMetal))
                        {
                            randAnimMetal = EruleRandom.RangeValue(1, 4);
                        }
                        metals.Add(randAnimMetal);
                        Play("metal" + randAnimMetal, from, to);
                    }
                    break;
                default:
                    Logger.Warning("[Animation] Element does not exist : " + elem._id);
                    return false;
            }
        }
        return true;
    }

    public bool PlayListSelf(List<Element> elemIds, Vector3 from)
    {
        Logger.Warning("Not implemented : spell effects self");
        return true;
    }

    public void PlayCharacterAnimation(List<Element> elems, Character character)
    {
        Dictionary<Element, int> nbElems = new Dictionary<Element, int>();
        for(int i = 0; i < _elementsHierarchy.Count; ++i)
        {
            nbElems.Add(_elementsHierarchy[i], 0);
        }

        int max = 0;
        for(int i = 0; i < elems.Count; ++i)
        {
            max = Mathf.Max(nbElems[elems[i]]++, max);
        }

        List<Element> chosen = new List<Element>();

        foreach(Element e in nbElems.Keys)
        {
            if (nbElems[e] == max)
                chosen.Add(e);
        }

        string triggerName = "Cast";
        if (chosen.Count == 1)
        {
             triggerName += chosen[0].ToString();
        }
        else
        {
            Element chosenElement = null;
            int priority = -0;
            for(int i = 0; i < chosen.Count; ++i)
            {
                if(chosenElement == null)
                {
                    chosenElement = chosen[i];
                    priority = _elementsHierarchy.IndexOf(chosenElement);
                }
                else
                {
                    if(_elementsHierarchy.IndexOf(chosen[i]) < priority)
                    {
                        chosenElement = chosen[i];
                        priority = _elementsHierarchy.IndexOf(chosenElement);
                    }
                }
            }
            triggerName += chosenElement.ToString();
        }

        character.GameObject.GetComponent<Animator>().SetTrigger("Cast");
        character.GameObject.GetComponent<Animator>().SetTrigger(triggerName);
    }

    public void PlayCharacterAnimationSelf(List<Element> elems, Character character)
    {
        bool isShield = true;
        for(int i = 0; i < elems.Count; ++i)
        {
            if(elems[i] != Element.GetElement(5))
            {
                isShield = false;
                break;
            }
        }

        if (isShield)
            character.GameObject.GetComponent<Animator>().SetTrigger("CastShield");
        else
            character.GameObject.GetComponent<Animator>().SetTrigger("CastSelf");

        character.GameObject.GetComponent<Animator>().SetTrigger("Cast");

    }
}
