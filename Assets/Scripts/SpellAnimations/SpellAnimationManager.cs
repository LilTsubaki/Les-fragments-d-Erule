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

    public bool Play(string id, GameObject from, GameObject to)
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

    public bool PlayList(HashSet<Element> elemIds, GameObject from, GameObject to)
    {
        foreach(Element elem in elemIds)
        {
            switch (elem._id)
            {
                case 0: // Fire
                    break;
                case 1: // Water
                    break;
                case 2: // Air
                    Play("air", from, to);
                    break;
                case 3: // Earth
                    break;
                case 4: // Wood
                    break;
                case 5: // Metal
                    int randNbMetal = EruleRandom.RangeValue(1, 3);
                    List<int> metals = new List<int>();
                    for(int i = 0; i < randNbMetal; ++i)
                    {
                        int randAnimMetal = EruleRandom.RangeValue(1, 3);
                        while (metals.Contains(randAnimMetal))
                        {
                            randAnimMetal = EruleRandom.RangeValue(1, 3);
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
