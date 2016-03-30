using UnityEngine;
using System.Collections.Generic;

public class SpellAnimationManager {

    static private SpellAnimationManager _instance;
    private Dictionary<string, SpellAnimation> _animations;

    private SpellAnimationManager()
    {
        _animations = new Dictionary<string, SpellAnimation>();
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
}
