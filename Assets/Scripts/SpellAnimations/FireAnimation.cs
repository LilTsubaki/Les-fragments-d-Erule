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
	public ParticleSystem _rocks;
    public float _forward;

    public bool _soundPlayed;

    void Update()
    {
        if (_play)
        {
            if (!_soundPlayed)
            {
                AudioManager.GetInstance().Play("spellFire", true, false);
            }

            gameObject.transform.position = _to;
            Vector3 projCam = new Vector3(Camera.main.transform.position.x, gameObject.transform.position.y, Camera.main.transform.position.z);
            gameObject.transform.LookAt(projCam);
            gameObject.transform.position = _to + gameObject.transform.forward * _forward;
            
            _fireAnimator.SetTrigger("play");
			_sparks.Play ();

            ParticleSystem.EmissionModule modExplosion = _explosion.emission;
            modExplosion.enabled = true;
            _explosion.Simulate(0, false, true);
			_explosion.Play ();

            ParticleSystem.EmissionModule modFlame = _flame.emission;
            modFlame.enabled = true;
            _flame.Simulate(0, true, true);
            _flame.Play();

            ParticleSystem.EmissionModule modMiniFlame1 = _miniFlame1.emission;
            modMiniFlame1.enabled = true;
            _miniFlame1.Simulate(0, true, true);
            _miniFlame1.Play();

            ParticleSystem.EmissionModule modMiniFlame2 = _miniFlame2.emission;
            modMiniFlame2.enabled = true;
            _miniFlame2.Simulate(0, true, true);
            _miniFlame2.Play();

			ParticleSystem.EmissionModule modRocks = _rocks.emission;
			modRocks.enabled = true;
			_rocks.Simulate (0, true, true);
			_rocks.Play ();

            _play = !_play;

        }
        if (_updateTimer)
        {
            timerUpdate();
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
        ParticleSystem.EmissionModule modExplo = _explosion.emission;
        modExplo.enabled = false;
        ParticleSystem.EmissionModule modFlame = _flame.emission;
        modFlame.enabled = false;
        ParticleSystem.EmissionModule modMiniFlame1 = _miniFlame1.emission;
        modMiniFlame1.enabled = false;
        ParticleSystem.EmissionModule modMiniFlame2 = _miniFlame2.emission;
        modMiniFlame2.enabled = false;
		ParticleSystem.EmissionModule modRocks = _rocks.emission;
		modRocks.enabled = false;
    }
}
