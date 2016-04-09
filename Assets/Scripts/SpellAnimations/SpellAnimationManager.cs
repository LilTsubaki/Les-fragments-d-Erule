﻿using UnityEngine;
using System.Collections.Generic;

public class SpellAnimationManager : Manager<SpellAnimationManager> {
    
    private Dictionary<string, SpellAnimation> _animations;
    private List<Element> _elementsHierarchy;

    private SpellTempo _tempo;

    public SpellAnimationManager()
    {
        if (_instance != null)
            throw new ManagerException();

        _animations = new Dictionary<string, SpellAnimation>();
        _elementsHierarchy = new List<Element>();
        _elementsHierarchy.Add(Element.GetElement(1));
        _elementsHierarchy.Add(Element.GetElement(4));
        _elementsHierarchy.Add(Element.GetElement(2));
        _elementsHierarchy.Add(Element.GetElement(5));
        _elementsHierarchy.Add(Element.GetElement(0));
        _elementsHierarchy.Add(Element.GetElement(3));
    }

    public bool Register(string id, SpellAnimation anim)
    {
        if (_animations.ContainsKey(id))
            return false;

        _animations.Add(id, anim);
        return true;
    }

    public void RegisterTempo(SpellTempo tempo)
    {
        _tempo = tempo;
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
        Debug.Log("Unknown animation " + id);
        return false;
    }

    public bool PlayList(List<Element> elemIds, Vector3 from, Vector3 to)
    {
        _tempo.SetPositions(from, to);

        //HashSet<Element> setElems = new HashSet<Element>(elemIds.ToArray());
        int nbMetal = elemIds.FindAll(delegate (Element e) { return e._id == 5; }).Count;//EruleRandom.RangeValue(1, 4);
        float totalTime = 0;

        List<Element> order = new List<Element>();
        for(int i = 0; i < _elementsHierarchy.Count; ++i)
        {
            if (elemIds.Contains(_elementsHierarchy[i]))
            {
                order.Add(_elementsHierarchy[i]);
            }
        }

        foreach (Element elem in order)
        {
            switch (elem._id)
            {
                case 0: // Fire
                    //Play("fire", from, to);
                    _tempo.PlayLater("Fire", totalTime);
                    totalTime += 1.2f;
                    break;
                case 1: // Water
                        //Play("water",from,to);
                    _tempo.PlayLater("Water", totalTime);
                    totalTime += 2.5f;
                    break;
                case 2: // Air
                    //Play("air", from, to);
                    _tempo.PlayLater("Air", totalTime);
                    totalTime += 1f;
                    break;
                case 3: // Earth
                    //Play("earth", from, to);
                    _tempo.PlayLater("Earth", totalTime);
                    break;
                case 4: // Wood
                    //Play("wood", from, to);
                    _tempo.PlayLater("Wood", totalTime);
                    totalTime += 1f;
                    break;
                case 5: // Metal
                    List<int> metals = new List<int>();
                    for(int i = 0; i < Mathf.Min(nbMetal, 3); ++i)
                    {
                        int randAnimMetal = EruleRandom.RangeValue(1, 3);
                        while (metals.Contains(randAnimMetal))
                        {
                            randAnimMetal = EruleRandom.RangeValue(1, 3);
                        }
                        metals.Add(randAnimMetal);
                        //Play("metal" + randAnimMetal, from, to);
                        _tempo.PlayLater("Metal" + randAnimMetal, totalTime);
                    }
                    totalTime += 2f;
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
        if(elemIds.Count >= 1)
        {
            Play("selfHalo", from, from);
        }
        if(elemIds.Count >= 2)
        {
            Play("selfParticles", from, from);
        }
        if(elemIds.Count >= 4)
        {
            Play("selfCircle", from, from);
        }
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
			max = Mathf.Max(++nbElems[elems[i]], max);
        }

        List<Element> chosen = new List<Element>();

        foreach(Element e in nbElems.Keys)
        {
			if (nbElems [e] == max)
				chosen.Add (e);
        }

        string triggerName = "Cast";
        if (chosen.Count == 1)
        {
             triggerName += chosen[0]._name;
        }
        else
        {
            Element chosenElement = null;
            /*int priority = -1;
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
            }*/

            chosenElement = chosen[EruleRandom.RangeValue(0, chosen.Count - 1)];


            triggerName += chosenElement._name;
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

		character.GameObject.GetComponent<Animator>().SetTrigger("Cast");
        if (isShield)
            character.GameObject.GetComponent<Animator>().SetTrigger("CastShield");
        else
            character.GameObject.GetComponent<Animator>().SetTrigger("CastSelf");


    }
}
