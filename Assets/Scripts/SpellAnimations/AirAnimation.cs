using UnityEngine;
using System.Collections;
using Xft;

public class AirAnimation : SpellAnimation {

    
    public float _initialScale;
    public float _finalScale;
	public ParticleSystem Clouds;
	public ParticleSystem Rocks;
	public GameObject _sprite;	
	public GameObject _cylinder;
    public float _speed;
	public float _height;
	public Xft.XWeaponTrail Trail1;
	public Xft.XWeaponTrail Trail2;

	// Update is called once per frame
	void FixedUpdate () {
        if (_play)
        {
            float distFromToNext = Vector3.Distance(_from, gameObject.transform.position);
            float distFromToTo = Vector3.Distance(_from, _to);
            float percentage = distFromToNext / distFromToTo;
            float scale = _initialScale * (1-percentage) + _finalScale * percentage;
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
			Vector3 next = Vector3.MoveTowards(transform.position, _to + new Vector3(0,_height,0) + gameObject.transform.forward * 0.5f, Time.deltaTime * _speed);
            transform.position = next;
			if(Vector3.Distance(next, _to + gameObject.transform.forward * 0.5f) < 1.1f)
            {
                _play = false;
                transform.localScale = new Vector3(_initialScale, _initialScale, _initialScale);
                _sprite.SetActive(false);
				_cylinder.SetActive(false);
				Clouds.Stop();
				Rocks.Stop();
            }
        }
	}

    public override void Reset()
    {
        transform.localScale = new Vector3(_initialScale, _initialScale, _initialScale);
		transform.position = new Vector3 (_from.x, _from.y + _height, _from.z);
        Vector3 look = new Vector3(_to.x, _from.y, _to.z);
        gameObject.transform.LookAt(look);
		_sprite.SetActive (true);
		_cylinder.SetActive (true);
		Clouds.Play();
		Rocks.Play();
		Trail1.Deactivate ();
		Trail2.Deactivate ();
		Trail1.Activate ();
		Trail2.Activate ();
    }

    
}
