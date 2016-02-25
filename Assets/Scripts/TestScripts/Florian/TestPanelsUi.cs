using UnityEngine;
using System.Collections;

public class TestPanelsUi : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        /*if (Input.GetKeyDown(KeyCode.R))
        {
            UIManager.GetInstance().FadeInPanel("Red");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIManager.GetInstance().FadeInPanel("Blue");
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            UIManager.GetInstance().HidePanel("Red");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            UIManager.GetInstance().HidePanel("Blue");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            UIManager.GetInstance().HideAll();
        }*/

        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.GetInstance().FadeInPanel("test");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            UIManager.GetInstance().FadeOutPanel("test");
        }
    }
}
