using UnityEngine;
using System.Collections;

public class Skydome_revol : MonoBehaviour {

	public GameObject Skydome;
	[Range(-1.0f,1.0f)]public float rotSpeed = -0.1f;
	private float Yrot = 0.0f;


	void FixedUpdate () {

		Yrot += rotSpeed;
		Skydome.transform.eulerAngles = new Vector3 (0.0f, Yrot, 0.0f);
	
	}
}
