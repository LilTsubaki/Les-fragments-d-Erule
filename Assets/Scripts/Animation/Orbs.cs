using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Orbs : MonoBehaviour {

    private Dictionary<int, Element> _elements;
    public bool _toCenter;
    private bool _slowingDown;
    private bool _goingCenter;

    public float SlowSpeed;
    public float GoingInSpeed;
    public float GoingOutSpeed;

    void Start()
    {
        _elements = new Dictionary<int, Element>();
    }

    void Update()
    {
        if (_toCenter)
        {
            Invoke("PauseAndGo", .5f);
            Invoke("RemoveAfterCast", 2);
            _toCenter = false;
            _slowingDown = true;
        }
        if (_slowingDown)
        {
            SlowRotationDown();
        }
        if(_goingCenter)
        {
            GoToCenter();
        }
        else
        {
            GoOut();
        }
    }

    private void PauseAndGo()
    {
        _goingCenter = true;
        _slowingDown = false;
    }

    private void RemoveAfterCast()
    {
        _goingCenter = false;
    }

    private void GoToCenter()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).childCount != 0)
            {
                transform.GetChild(i).GetChild(0).position = Vector3.MoveTowards(transform.GetChild(i).GetChild(0).position, transform.position, Time.deltaTime*GoingInSpeed);
            }
        }
    }

    private void SlowRotationDown()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).GetComponent<RotatingOrb>()._rotationSpeed = Mathf.Lerp(transform.GetChild(i).GetComponent<RotatingOrb>()._rotationSpeed, 0, Time.deltaTime*SlowSpeed);
        }
    }

    private void GoOut()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).childCount != 0)
            {
                transform.GetChild(i).GetChild(0).position = Vector3.MoveTowards(transform.GetChild(i).GetChild(0).position, transform.GetChild(i).position, Time.deltaTime*GoingOutSpeed);
            }
            RotatingOrb rotate = transform.GetChild(i).GetComponent<RotatingOrb>();
            rotate._rotationSpeed = Mathf.Lerp(rotate._rotationSpeed, rotate._initialSpeed, Time.deltaTime*3);
        }
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
