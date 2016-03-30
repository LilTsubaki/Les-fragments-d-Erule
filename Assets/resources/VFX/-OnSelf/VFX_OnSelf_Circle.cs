using UnityEngine;
using System.Collections;


public class VFX_OnSelf_Circle : MonoBehaviour {
    	
		private float rot2 = 0.0f;
        [Range(-3.0f,3.0f)]public float rotSpeed2 = -1.9f;

    void FixedUpdate()
		{	
			rot2 += rotSpeed2;			
			gameObject.transform.eulerAngles = new Vector3 (0.0f,rot2,0.0f);	// replace the X coordinates by rot
		}

}