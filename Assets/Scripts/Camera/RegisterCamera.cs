using UnityEngine;
using System.Collections;

public class RegisterCamera : MonoBehaviour
{

    public string cameraName;

    void Start()
    {
        CameraManager.GetInstance().RegisterCamera(gameObject, cameraName);
    }
}
