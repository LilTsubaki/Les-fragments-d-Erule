using UnityEngine;
using System.Collections;

public class testSound : MonoBehaviour
{


	// Use this for initialization
	void Start ()
    {
        AudioManager.GetInstance().PlayLoopingClips("fire");
    }
	
	// Update is called once per frame
	void Update ()
    {
        	
	}
}
