using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager
{

    private Dictionary<string, GameObject> cameras;
    static private CameraManager instance;
    private Camera active;
    RegisterFadeScreen fadescreen;

    string nextCamera;
    bool isStable = true;

    private CameraManager()
    {
        cameras = new Dictionary<string, GameObject>();
    }

    public static CameraManager GetInstance()
    {
        if (instance == null)
        {
            instance = new CameraManager();
        }
        return instance;
    }

    public bool RegisterCamera(GameObject c, string name)
    {
        if (!cameras.ContainsKey(name))
        {
            cameras.Add(name, c);
            if (active == null)
            {
                active = c.GetComponent<Camera>();
                active.enabled = true;
            }
            else
            {
                c.GetComponent<Camera>().enabled = false;
            }
            return true;
        }
        return false;
    }

    public void SetFadeScreen(RegisterFadeScreen screen)
    {
        fadescreen = screen;
    }

    public void AroundY(Vector3 center, float speed)
    {
        active.transform.LookAt(center, Vector3.up);
        active.transform.RotateAround(center, Vector3.up, speed * Time.timeScale);
    }

    public void LookAt(GameObject go)
    {
        active.transform.LookAt(go.transform);
    }

    public void MoveForward(float speed)
    {
        Transform t = active.transform;
        MoveTo(t.position + t.forward, speed);
    }

    public void MoveBackward(Vector3 pos, float speed)
    {
        Transform t = active.transform;
        MoveTo(t.position - t.forward, speed);
    }

    public void MoveTo(Vector3 pos, float speed)
    {
        Vector3 p = Vector3.MoveTowards(active.transform.position, pos, speed);
        active.transform.position = p;
    }

    public void Fov(float angle)
    {
        active.fieldOfView = angle;
    }

    public bool StartAnimation()
    {
        Animator anim = active.gameObject.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("Go", true);
            return true;
        }
        return false;
    }

    public void FadeTo(string cameraName)
    {
        if (cameras.ContainsKey(cameraName))
        {
            isStable = false;
            fadescreen.Reverse();
            nextCamera = cameraName;
            fadescreen.FadeTime();
        }
        else
            Debug.Log("Does not exist : " + cameraName);
    }

    public void ActivateMain()
    {
        active.enabled = false;
        active = cameras[nextCamera].GetComponent<Camera>();
        active.enabled = true;
        nextCamera = "";
        Debug.Log(StartAnimation());
        isStable = true;
    }

    public bool IsStable()
    {
        return isStable;
    }

}
