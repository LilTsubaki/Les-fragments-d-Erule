using UnityEngine;
using System.Collections;


public class VFX_Wood_ThorncrownRotate : MonoBehaviour {

		//public GameObject Thorncrown;
			
		private float Yrot,rot1,tilt1 = 0.0f;
        [Range(-30.0f,30.0f)]public float rotSpeed1 = 3.0f;
		[Range(-30.0f,30.0f)]public float tiltSpeed1 = 10.0f;


    void Start()
        {
            Yrot = /*Thorncrown*/gameObject.transform.eulerAngles.y;
            Debug.Log(Yrot);
        }
    void FixedUpdate()
		{	
			rot1 += rotSpeed1;
			tilt1 = Mathf.Sin (Time.time) * tiltSpeed1;
				
			/*Thorncrown*/gameObject.transform.eulerAngles = new Vector3 (rot1,Yrot,tilt1);	// replace the Y coordinates by rot
		}

}