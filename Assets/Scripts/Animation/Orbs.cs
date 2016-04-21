using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Orbs : MonoBehaviour {

    private Dictionary<int, Element> _elements;
    private List<GameObject> _runesGO;
    public bool _successCast;
    private bool _goingSuccess;
    private bool _slowingDown;
    private bool _goingCenter;

    public bool _failCast;
    private bool _goingFail;
    private bool _accelerate;

    public float _slowDuration;
    public float _slowSpeed;
    public float _gatheringDuration;
    public float _goingInSpeed;
    public float _goingOutSpeed;

    public float _failAcceleration;
    public float _failMaxSpeed;
    public float _failDuration;

    void Start()
    {
        _elements = new Dictionary<int, Element>();
        _runesGO = new List<GameObject>();

        List<Element> elements = Element.GetElements();
        for (int i = 0; i < elements.Count; i++)
        {
            GameObject orb = Resources.Load<GameObject>("VFX/-Runes_Orbs/Rune" + elements[i]._name);
            Logger.Debug(elements[i]._id + " " + elements[i]._name);
            GameObject newRune = Instantiate(orb);
            _runesGO.Add(newRune);
            newRune.SetActive(false);
        }
        Logger.Debug("Count " + _runesGO.Count);
    }

    void FixedUpdate()
    {
        if (_successCast)
        {
            Invoke("PauseAndGo", _slowDuration);
            Invoke("RemoveAfterCast", _slowDuration + _gatheringDuration);
            Invoke("StopSuccess", _slowDuration + _gatheringDuration * 2);
            _successCast = false;
            _slowingDown = true;
            _goingSuccess = true;
        }
        if (_goingSuccess)
        {
            if (_slowingDown)
            {
                SlowRotationDown();
            }
            if (_goingCenter)
            {
                GoToCenter();
            }
            else
            {
                GoOut();
            }
        }

        if (_failCast)
        {
            Invoke("SlowDownFail", _failDuration);
            Invoke("StopFail", _failDuration * 2.1f);
            _accelerate = true;
            _goingFail = true;
            _failCast = false;
        }
        if (_goingFail)
        {
            if (_accelerate)
            {
                AccelerateFail();
            }
            else
            {
                DecelerateFail();
            }
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
        ActivateRunes(true);
    }

    private void StopSuccess()
    {
        _goingSuccess = false;
    }

    private void SlowDownFail()
    {
        _accelerate = false;
    }

    private void StopFail()
    {
        _goingFail = false;
    }


    public void ActivateRunes(bool active)
    {
        if(!_goingFail)
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                _runesGO[_elements[i]._id].SetActive(active);
            }
        }
            /*for (int i = 0; i < transform.childCount; ++i)
            {
                if (transform.GetChild(i).childCount != 0)
                {
                    transform.GetChild(i).GetChild(0).gameObject.SetActive(active);
                }
            }*/
    }

    private void GoToCenter()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).childCount != 0)
            {
                transform.GetChild(i).GetChild(0).position = Vector3.MoveTowards(transform.GetChild(i).GetChild(0).position, transform.position, Time.deltaTime*_goingInSpeed);
            }
        }
    }

    private void SlowRotationDown()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).GetComponent<RotatingOrb>()._rotationSpeed = Mathf.Lerp(transform.GetChild(i).GetComponent<RotatingOrb>()._rotationSpeed, 0, Time.deltaTime*_slowSpeed);
        }
    }

    private void GoOut()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).childCount != 0)
            {
                transform.GetChild(i).GetChild(0).position = Vector3.MoveTowards(transform.GetChild(i).GetChild(0).position, transform.GetChild(i).position, Time.deltaTime*_goingOutSpeed);
            }
            RotatingOrb rotate = transform.GetChild(i).GetComponent<RotatingOrb>();
            rotate._rotationSpeed = Mathf.Lerp(rotate._rotationSpeed, rotate._initialSpeed, Time.deltaTime*3);
        }
    }

    
    private void AccelerateFail()
    { 
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).GetComponent<RotatingOrb>()._rotationSpeed = Mathf.Lerp(transform.GetChild(i).GetComponent<RotatingOrb>()._rotationSpeed, _failMaxSpeed, Time.deltaTime * _failAcceleration);
        }
    }

    private void DecelerateFail()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            RotatingOrb rotate = transform.GetChild(i).GetComponent<RotatingOrb>();
            rotate._rotationSpeed = Mathf.Lerp(rotate._rotationSpeed, rotate._initialSpeed, Time.deltaTime * _failAcceleration);
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
                Logger.Debug("Get : " + elem._id);
                _runesGO[elem._id].SetActive(true);
                _runesGO[elem._id].transform.SetParent(gameObject.transform.GetChild(i));
                _runesGO[elem._id].transform.localPosition = Vector3.zero;
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
                //Destroy(gameObject.transform.GetChild(key).GetChild(0).gameObject);
                Logger.Debug("Delete : " + elem._id);
                _runesGO[elem._id].SetActive(false);
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
            Logger.Debug("Remove : " + diff[i]._name);
            RemoveElement(diff[i]);
        }

        for(int i = 0; i < elems.Count; ++i)
        {
            Logger.Debug("Add : " + elems[i]._name);
            AddElement(elems[i]);
        }
    }
}
