using UnityEngine;
using System.Collections;

public class TestCamera : MonoBehaviour {

    bool started;
    public GameObject aim;

	// Use this for initialization
	void Start () {
        started = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            CameraManager.GetInstance().ZoomInPerspective(2, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            CameraManager.GetInstance().ZoomInOrthographic(5, 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            CameraManager.GetInstance().ZoomOutPerspective(2, 1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            CameraManager.GetInstance().ZoomOutOrthographic(5, 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            CameraManager.GetInstance().LookAt(aim);
        }

        //CameraManager.GetInstance().AroundY(aim.transform.position, 2);

        /*if (started)
        {
            GetFirst();
            started = false;
        }//*/
    }

    void GetFirst()
    {
        CameraManager.GetInstance().FadeTo("First", 0.2f);
        Invoke("GetSecond", 3);
    }

    void GetSecond()
    {
        CameraManager.GetInstance().FadeTo("Second", 0.2f);
        Invoke("GetFirst", 3);
    }
}
