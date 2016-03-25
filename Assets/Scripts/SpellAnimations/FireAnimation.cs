using UnityEngine;
using System.Collections;

public class FireAnimation : SpellAnimation
{
    public Animator _fireAnimator;
	public Animator _fireSprite;
	public Animator _explosionAnimator;
	public Animator _godraybisAnimator;
	public Animator _impactAnimator;
	public Animator _impactMiniAnimator;
	public ParticleSystem _sparks;
	public ParticleSystem _explosion;
    public ParticleSystem _flame;
    public ParticleSystem _miniFlame1;
    public ParticleSystem _miniFlame2;
    public float _forward;

    void Update()
    {
        if (_play)
        {
            gameObject.transform.position = _to.transform.position;
            Vector3 projCam = new Vector3(Camera.main.transform.position.x, gameObject.transform.position.y, Camera.main.transform.position.z);
            gameObject.transform.LookAt(projCam);
            gameObject.transform.position = _to.transform.position + gameObject.transform.forward * _forward;
            
            _fireAnimator.SetTrigger("play");
			_sparks.Play ();

            _explosion.enableEmission = true;
            _explosion.Simulate(0, false, true);
			_explosion.Play ();

            _flame.enableEmission = true;
            _flame.Simulate(0, true, true);
            _flame.Play();

            _miniFlame1.enableEmission = true;
            _miniFlame1.Simulate(0, true, true);
            _miniFlame1.Play();

            _miniFlame2.enableEmission = true;
            _miniFlame2.Simulate(0, true, true);
            _miniFlame2.Play();

            _play = !_play;
        }
    }

	public void PlayExplosionRay()
	{
		_explosionAnimator.SetTrigger ("play");
	}

	public void PlayGodRayBis()
	{
		_godraybisAnimator.SetTrigger ("play");
	}

	public void Impact()
	{
		_impactAnimator.SetTrigger ("play");
	}

	public void ImpactMini()
	{
		_impactMiniAnimator.SetTrigger ("play");
	}

    public void ResetBursts()
    {
        _explosion.enableEmission = false;
        _flame.enableEmission = false;
        _miniFlame1.enableEmission = false;
        _miniFlame2.enableEmission = false;
    }
}
