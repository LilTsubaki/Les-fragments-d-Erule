using UnityEngine;
using System.Collections;
using Xft;

public class MetalAnimation : SpellAnimation
{
    public Animator _anim;
    public RotatoryAxe _axe;
    public GameObject _trail;

	// Update is called once per frame
	void Update () {

        if (_play)
        {
            _axe.RandomInclination();
            Reset();
            gameObject.transform.position = _to + new Vector3(0,.6f,0);
            
            _anim.SetTrigger("play");
            _play = !_play;
            
        }
	}

    public override void Reset()
    {
        _trail.SetActive(false);
        transform.position = _from;
        Vector3 look = new Vector3(_to.x, _from.y, _to.z);
        gameObject.transform.LookAt(look);
        _trail.SetActive(true);
    }
}
