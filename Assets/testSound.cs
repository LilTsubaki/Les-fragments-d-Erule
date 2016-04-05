using UnityEngine;
using System.Collections;

public class testSound : MonoBehaviour
{


	// Use this for initialization
	void Start ()
    {
        AudioManager.GetInstance().PlayFadeIn("fire", true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        	
	}
}
