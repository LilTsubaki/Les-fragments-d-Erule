using UnityEngine;
using System.Collections;

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
            gameObject.transform.position = _to.transform.position + new Vector3(0,.6f,0);
            
            _anim.SetTrigger("play");
            _play = !_play;
            
        }
	}

    public override void Reset()
    {
        _trail.SetActive(false);
        transform.position = _from.transform.position;
        Vector3 look = new Vector3(_to.transform.position.x, _from.transform.position.y, _to.transform.position.z);
        gameObject.transform.LookAt(look);
        _trail.SetActive(true);
        //_trail.GetComponent<XWeaponTrailDemo>().
    }
}
