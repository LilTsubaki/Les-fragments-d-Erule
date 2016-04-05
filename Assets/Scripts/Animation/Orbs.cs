using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Orbs : MonoBehaviour {

    private Dictionary<int, Element> _elements;

    void Start()
    {
        _elements = new Dictionary<int, Element>();
    }

    public void AddElement(Element elem)
    {
        if(_elements.ContainsValue(elem))
        {
            return;
        }

        for(int i = 0; i < 6; ++i)
        {
            if (!_elements.ContainsKey(i))
            {
                _elements.Add(i, elem);
                GameObject orb = Resources.Load<GameObject>("VFX/-Runes_Orbs/Rune" + elem._name);
                GameObject newRune = Instantiate(orb);
                newRune.transform.SetParent(gameObject.transform.GetChild(i));
                newRune.transform.localPosition = Vector3.zero;
                break;
            }
        }
        Logger.Debug("Added " + elem._name);

    }

    public void RemoveElement(Element elem)
    {
        if (!_elements.ContainsValue(elem))
        {
            return;
        }

        int id = -1;
        for(int i = 0; i < _elements.Keys.Count; ++i)
        {
            int key = _elements.Keys.ElementAt(i);
            if (_elements[key] == elem)
            {
                id = key;
                Destroy(gameObject.transform.GetChild(key).GetChild(0).gameObject);
                Logger.Debug("Removed " + key);
                break;
            }
        }
        _elements.Remove(id);
    }

    public void SetElements(List<Element> elems)
    {
        List<Element> already = _elements.Values.ToList();
        List<Element> diff = already.Except(elems).ToList();
        
        for(int i = 0; i < diff.Count; ++i)
        {
            RemoveElement(diff[i]);
        }

        for(int i = 0; i < elems.Count; ++i)
        {
            AddElement(elems[i]);
        }
    }
}
