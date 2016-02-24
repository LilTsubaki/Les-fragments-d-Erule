using UnityEngine;
using System.Collections;

public class TestCastAnimation : MonoBehaviour {


    public AirAnimation anim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // UNDERWHELMING
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.Reset();
            anim.Play();
        }
    }
}
