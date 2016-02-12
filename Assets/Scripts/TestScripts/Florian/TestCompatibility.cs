using UnityEngine;
using System.Collections;

public class TestCompatibility : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RunicBoardManager.GetInstance().GetCompatibility(Element.GetElement(0), Element.GetElement(0));
        }
	}
}
