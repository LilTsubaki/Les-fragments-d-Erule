using UnityEngine;
using System.Collections;

public class SpellAnimation : MonoBehaviour {

    public Vector3 _from;
    public Vector3 _to;
    public bool _play;

    public string _registerName;

    // Use this for initialization
    void Start () {
        SpellAnimationManager.GetInstance().Register(_registerName, this);
        gameObject.SetActive(false);
	}

    public void Reset(Vector3 from, Vector3 to)
    {
        _from = from;
        _to = to;
        _play = false;
        Reset();
    }

    public virtual void Reset() {
        transform.position = _from;
        Vector3 look = new Vector3(_to.x, _from.y, _to.z);
        gameObject.transform.LookAt(look);
    }

    public void Play()
    {
        _play = true;
        gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        SpellAnimationManager.GetInstance().Remove(_registerName);
    }

}
