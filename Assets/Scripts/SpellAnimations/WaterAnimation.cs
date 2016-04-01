using UnityEngine;
using System.Collections;


public class WaterAnimation : SpellAnimation 
{
	
	public ParticleSystem _splashesBottom;
	public ParticleSystem _splashesTop;
	public ParticleSystem _water;
    public Renderer _waterMat;

    [Range(0, 1)]
    public float _cutoff;

	// Update is called once per frame
	void Update () {
		if (_play) {
			gameObject.transform.position = _to;
			Vector3 projCam = new Vector3 (Camera.main.transform.position.x, gameObject.transform.position.y, Camera.main.transform.position.z);
			gameObject.transform.LookAt (projCam);
            _waterMat.material.SetFloat("_Cutoff", 1 - _cutoff);

            /*
			ParticleSystem.EmissionModule modsplashesBottom = _splashesBottom.emission;
			modsplashesBottom.enabled = true;
			_splashesBottom.Simulate (0, false, true);
			_splashesBottom.Play ();

			ParticleSystem.EmissionModule modsplashesTop = _splashesTop.emission;
			modsplashesTop.enabled = true;
			_splashesTop.Simulate (0, false, true);
			_splashesTop.Play ();

			ParticleSystem.EmissionModule modwater = _water.emission;
			modwater.enabled = true;
			_water.Simulate (0, false, true);
			_water.Play ();

		}
	}
		public void ResetBursts()
		{
		ParticleSystem.EmissionModule modsplashesBottom = _splashesBottom.emission;
		modsplashesBottom.enabled = false;
		ParticleSystem.EmissionModule modsplashesTop = _splashesTop.emission;
		modsplashesTop.enabled = false;
		ParticleSystem.EmissionModule modwater = _splashesTop.emission;
		modwater.enabled = false;*/
        }
	}
}

