using UnityEngine;
using System.Collections;

public class LogoAnimation : MonoBehaviour {

	public Animator SM_E;
	public Animator SM_E_pan;
	public Animator SM_Bordure_E;
	public Animator SM_Logo_Rock_2;
	public Animator SM_Logo_Rock_3;
	public Animator SM_Logo_Rock_4;
	public Animator SM_Logo_Rock_5;
	public Animator SM_Logo_Rock_6;
	public Animator SM_Logo_Rock_7;
	public Animator SM_Logo_Rock_8;
	public Animator SM_Logo_Rock_9;
	public Animator SM_Logo_Rock_10;
	public Animator SM_Logo_Rock_11;
	public ParticleSystem Particle_Energy;
	public ParticleSystem Particle_Title;


	public FloatingObjects _logo;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.L)) 
			{
			TitleDisappear ();
			}
	}

	private void SetFloatingLogo() {
		_logo.enabled = true;
	}

	private void TitleDisappear(){
		Particle_Title.Play ();
		Invoke ("EnergyBall", 2f);
	}

	private void EnergyBall(){
		Particle_Energy.Play ();
		Particle_Energy.gameObject.GetComponent<Animator> ().enabled = true;
		Invoke ("BuildLogo", 4.3f);
	}

	private void BuildLogo(){
		Particle_Energy.Stop ();
		SM_E.SetTrigger ("play");
		SM_E_pan.SetTrigger ("play");
		SM_Bordure_E.SetTrigger ("play");
		SM_Logo_Rock_2.SetTrigger ("play");
		SM_Logo_Rock_3.SetTrigger ("play");
		SM_Logo_Rock_4.SetTrigger ("play");
		SM_Logo_Rock_5.SetTrigger ("play");
		SM_Logo_Rock_6.SetTrigger ("play");
		SM_Logo_Rock_7.SetTrigger ("play");
		SM_Logo_Rock_8.SetTrigger ("play");
		SM_Logo_Rock_9.SetTrigger ("play");
		SM_Logo_Rock_10.SetTrigger ("play");
		SM_Logo_Rock_11.SetTrigger ("play");

		Invoke ("SetFloatingLogo", 3f);
	}
}
