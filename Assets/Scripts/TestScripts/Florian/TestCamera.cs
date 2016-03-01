using UnityEngine;
using System.Collections;

public class TestCamera : MonoBehaviour {

    bool started;
    public float speed;
    public GameObject aim;

	// Use this for initialization
	void Start () {
        started = true;
	}
	
	// Update is called once per frame
	void Update () {

        CameraManager.GetInstance().AroundY(aim.transform.position, speed);

        if (started)
        {
            started = false;
        }
        if(Input.GetMouseButtonDown(0))
        {
            CameraManager.GetInstance().FadeTo("First", 0.1f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            CameraManager.GetInstance().FadeTo("Second", 0.1f);
        }
    }

}
