﻿using UnityEngine;
using System.Collections;

public class WoodAnimation : SpellAnimation {

    public GameObject _thorn1;
    public GameObject _thorn2;
    public GameObject _thorn3;
    public GameObject _cracks;
    public ParticleSystem _clouds;
    public ParticleSystem _rocks;
    public float _speed;

    void Update()
    {
        if (_play)
        {
            Vector3 next = Vector3.MoveTowards(transform.position, _to + gameObject.transform.forward * 0.5f, Time.deltaTime * _speed);
            transform.position = next;
            if (Vector3.Distance(next, _to + gameObject.transform.forward * 1f) < 1.1f)
            {
                _play = false;
                _thorn1.SetActive(false);
                _thorn2.SetActive(false);
                _thorn3.SetActive(false);
                _cracks.SetActive(false);
                _clouds.Stop();
                _rocks.Stop();
            }
        }
    }

    public override void Reset()
    {
        transform.position = new Vector3(_from.x, _from.y, _from.z);
        Vector3 look = new Vector3(_to.x, _from.y, _to.z);
        gameObject.transform.LookAt(look);
        _thorn1.SetActive(true);
        _thorn2.SetActive(true);
        _thorn3.SetActive(true);
        _cracks.SetActive(true);
        _clouds.Play();
        _rocks.Play();
    }

}
