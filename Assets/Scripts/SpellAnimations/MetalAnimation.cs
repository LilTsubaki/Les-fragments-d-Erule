using UnityEngine;
using System.Collections;

public class MetalAnimation : SpellAnimation
{
    public Animator _anim;
    public RotatoryAxe _axe;

	// Update is called once per frame
	void Update () {

        if (_play)
        {
            _axe.RandomInclination();
            Reset();
            gameObject.transform.position = _to.transform.position;
            _anim.SetTrigger("play");
            _play = !_play;
            
        }
	}
}
