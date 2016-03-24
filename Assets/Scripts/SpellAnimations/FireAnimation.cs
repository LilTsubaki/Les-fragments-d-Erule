using UnityEngine;
using System.Collections;

public class FireAnimation : SpellAnimation
{
    public Animator _fireAnimator;
	public Animator _fireSprite;
	public Animator _explosionAnimator;
	public Animator _godraybisAnimator;
	public ParticleSystem _sparks;
	public ParticleSystem _explosion;
    public float _forward;

    void Update()
    {
        if (_play)
        {
            Vector3 projCam = new Vector3(Camera.main.transform.position.x, gameObject.transform.position.y, Camera.main.transform.position.z);
            gameObject.transform.LookAt(projCam);
            gameObject.transform.position = _to.transform.position + gameObject.transform.forward * _forward;
            _fireAnimator.SetTrigger("play");
			_sparks.Play ();
			_explosion.Play ();
            _play = !_play;
        }
    }

	public void PlayFireSprite(){
		_fireSprite.SetTrigger ("play");
	}

	public void PlayExplosionRay()
	{
		_explosionAnimator.SetTrigger ("play");
	}

	public void PlayGodRayBis()
	{
		_godraybisAnimator.SetTrigger ("play");
	}
}
