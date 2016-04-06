using UnityEngine;
using System.Collections;

public class PresentationFadeCamera : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}

	public void callFadeOut(){
		CameraManager.GetInstance ().FadeTo ("cameraBoard",1.0f);
	}

}
