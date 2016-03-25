using UnityEngine;
using System.Collections;

public class SpellAnimation : MonoBehaviour {

    public GameObject _from;
    public GameObject _to;
    public bool _play;

    public string _registerName;

    // Use this for initialization
    void Start () {
        SpellAnimationManager.GetInstance().Register(_registerName, this);
        gameObject.SetActive(false);
	}

    public void Reset(GameObject from, GameObject to)
    {
        _from = from;
        _to = to;
        _play = false;
        Reset();
    }

    public virtual void Reset() {
        transform.position = _from.transform.position;
        Vector3 look = new Vector3(_to.transform.position.x, _from.transform.position.y, _to.transform.position.z);
        gameObject.transform.LookAt(look);
    }

    public void Play()
    {
        _play = true;
        gameObject.SetActive(true);
    }

}
