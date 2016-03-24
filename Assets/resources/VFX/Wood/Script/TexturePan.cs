using UnityEngine;
using System.Collections;

public class TexturePan : MonoBehaviour {

	public float speedU = -0.5F;
	public float speedV = -1.0F;
    public float length = 1.0F;
    /*public float panSpeed = 3.0f;
	
	private bool panBoost = false; */

    void Update() 
	{
	
		/*if (panBoost==true && (Input.GetButtonDown ("Use")))
		{
			speedU *= panSpeed;
			speedV *= panSpeed;	
		}
		*/
		Vector2 offset = /*Mathf.Repeat(Time.time, 4.0f) * */ new Vector2(speedU, speedV) * (length * Time.time);
		GetComponent<Renderer>().material.mainTextureOffset = offset;
		
	}

	
	/*void OnTriggerEnter(Collider other)
	{	
		
		if (other.gameObject.tag == "Bellows_Trigger") 
		{	
			panBoost = true;
		}
	
	}
	
	
	void OnTriggerExit(Collider other)
	{	

		panBoost = false;
		
	}
	*/
}

	

	
	
	
	
	

	
	
