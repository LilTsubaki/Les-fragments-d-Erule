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
    }

}
