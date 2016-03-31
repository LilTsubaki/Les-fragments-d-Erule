using UnityEngine;
using System.Collections;


public class VFX_Channel_Sparks: MonoBehaviour {

    //public GameObject Object;

    //[Range(0.0f,5.0f)]public float speed = 0.0f;
    //private float YPos,minYPos,maxYPos,oldYPos = 0.0f;

    public Transform target;
    public GameObject spark;

    private float rot3,/*tilt,*/lift3,scale3 = 0.0f;
	[Range(-4.0f,4.0f)]public float rot3Speed = 0.05f;
	//[Range(-4.0f,4.0f)]public float tiltSpeed = 0.5f;
	[Range(-0.1f,0.1f)]public float lift3Speed = 0.012f;
	//[Range(-10.0f,10.0f)]public float revolve = 1.1f;
    [Range(0.0f,10.0f)]public float scale3Speed = 1.0f;
		
			
	void FixedUpdate()
	{	
		//if (enabled)
		//{
		rot3 += rot3Speed /** revolve*/;
		//tilt = Mathf.Sin (Time.time) * tiltSpeed;
		lift3 = Mathf.Sin (Time.time) * lift3Speed;
        scale3 = Mathf.Sin (-(Time.time)) * scale3Speed;

				
		spark.transform.localEulerAngles = new Vector3 (rot3,0.0f,0.0f);  // replace the Y coordinates by rot
        spark.transform.Translate(0.0f,lift3,0.0f, Space.Self);
        spark.transform.localScale = new Vector3(scale3, scale3, scale3);
        //Object.transform.RotateAround(Vector3.zero, Vector3.up, revolve);
        transform.LookAt(target, Vector3.one);
        //}
    }
}