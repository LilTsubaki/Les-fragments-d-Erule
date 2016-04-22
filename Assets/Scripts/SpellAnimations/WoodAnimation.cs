using UnityEngine;
using System.Collections;

public class WoodAnimation : SpellAnimation {

    public GameObject _thorn1;
    public GameObject _thorn2;
    public GameObject _thorn3;
    public GameObject _cracks;
    public ParticleSystem _clouds;
    public ParticleSystem _rocks;
    public float _speed;
    public bool _timeToHitTargetRegistered = false;

    private bool _soundPlayed;
    private int _idPlayerThorns;
    private int _idPlayerTrail;


    void FixedUpdate()
    {
        if (_play)
        {
            if (!_soundPlayed)
            {
                _idPlayerThorns = AudioManager.GetInstance().Play("spellWood", true, false);
                _idPlayerTrail = AudioManager.GetInstance().Play("spellWoodTrail", true, false);
                _soundPlayed = true;
            }
            Vector3 next = Vector3.MoveTowards(transform.position, _to + new Vector3(0f,0.2f,0f) + gameObject.transform.forward * 0.5f, Time.deltaTime * _speed);
            transform.position = next;
            if (!_timeToHitTargetRegistered)
            {
                float distFromToTo = Vector3.Distance(_from, _to);
                _timeToHitTarget = distFromToTo / 35.0f;
                _timeToHitTargetRegistered = true;
            }
            if (Vector3.Distance(next, _to + gameObject.transform.forward * 1f) < 1.1f)
            {
                _play = false;
                _thorn1.SetActive(false);
                _thorn2.SetActive(false);
                _thorn3.SetActive(false);
                _cracks.SetActive(false);
                _clouds.Stop();
                _rocks.Stop();

                _soundPlayed = false;
                AudioManager.GetInstance().FadeOut(_idPlayerThorns);
                AudioManager.GetInstance().FadeOut(_idPlayerTrail);

            }
        }
        if(_updateTimer)
        {
            timerUpdate();
        }
    }

    public override void Reset()
    {
        transform.position = new Vector3(_from.x, _from.y + 0.2f, _from.z);
        Vector3 look = new Vector3(_to.x, _from.y + 0.2f, _to.z);
        gameObject.transform.LookAt(look);
        _thorn1.SetActive(true);
        _thorn2.SetActive(true);
        _thorn3.SetActive(true);
        _cracks.SetActive(true);
        _clouds.Play();
        _rocks.Play();
        _timeToHitTargetRegistered = false;
        _soundPlayed = false;
    }

}
