using UnityEngine;
using System.Collections;

public class TestCastAnimation : MonoBehaviour {

    public GameObject _cube1;
    public GameObject _cube2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // UNDERWHELMING
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpellAnimationManager.GetInstance().Play("air", _cube1, _cube2);
        }
    }
}
