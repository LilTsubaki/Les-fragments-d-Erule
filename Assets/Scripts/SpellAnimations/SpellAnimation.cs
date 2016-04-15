using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellAnimation : MonoBehaviour {

    public Vector3 _from;
    public Vector3 _to;
    public bool _play;
    public bool _updateTimer;

    public float _timeToHitTarget;
    public float _timer;
    public List<Hexagon> _hexagons;

    public string _registerName;

    // Use this for initialization
    public void Start () {
        SpellAnimationManager.GetInstance().Register(_registerName, this);
        gameObject.SetActive(false);
	}

    public void Reset(Vector3 from, Vector3 to, List<Hexagon> hexagons)
    {
        _from = from;
        _to = to;
        _play = false;
        _hexagons = hexagons;
        Reset();
    }

    public virtual void Reset() {
        transform.position = _from;
        Vector3 look = new Vector3(_to.x, _from.y, _to.z);
        gameObject.transform.LookAt(look);
    }

    public void timerUpdate()
    {
        _timer += Time.deltaTime;
        Logger.Debug(gameObject.name + " " + _timeToHitTarget + " " + _timer);
        if (_timer >= _timeToHitTarget && _hexagons != null)
        {
            for (int i = 0; i < _hexagons.Count; i++)
            {
                Entity entity = _hexagons[i]._entity;
                if(entity != null)
                {
                    EffectUIManager.GetInstance().Unpause(entity);
                }

                if(entity is Character)
                {
                    Character c = (Character)entity;
                    c.GameObject.GetComponent<Animator>().SetTrigger("Hit");
                }
            }
            _timer = 0.0f;
            _updateTimer = false;
        }
    }

    public void Play()
    {
        gameObject.SetActive(true);
        _updateTimer = true;
        _play = true;
    }

    void OnDestroy()
    {
        SpellAnimationManager.GetInstance().Remove(_registerName);
    }

}
