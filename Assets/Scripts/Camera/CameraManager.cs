using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The class managing the Cameras. Cameras have to be registered with the script RegisterCamera.
/// </summary>
public class CameraManager
{
    /// <summary>
    /// The mapping between cameras' names and the Game Objects of the registered Cameras.
    /// </summary>
    private Dictionary<string, GameObject> _cameras;
    /// <summary>
    /// The only instance of CameraManager.
    /// </summary>
    static private CameraManager _instance;
    /// <summary>
    /// The active Camera the CameraManager acts on.
    /// </summary>
    private Camera _active;
    /// <summary>
    /// The Panel used to fade between Cameras.
    /// </summary>
    RegisterFadeScreen _fadescreen;

    /// <summary>
    /// The name of the active Camera.
    /// </summary>
    string _activeName;
    /// <summary>
    /// The name of the Camera we want to fade to.
    /// </summary>
    string _nextCamera;
    /// <summary>
    /// If the CameraManager has finished its transition between two Cameras.
    /// </summary>
    bool _isStable = true;

    /// <summary>
    /// Constructor of CameraManager. Must be used in GetInstance only.
    /// </summary>
    private CameraManager()
    {
        _cameras = new Dictionary<string, GameObject>();
    }

    /// <summary>
    /// Gets the only instance of the CameraManager. Creates the instance if doesn't already exists.
    /// </summary>
    /// <returns>The only instance of hte CameraManager.</returns>
    public static CameraManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new CameraManager();
        }
        return _instance;
    }

    /// <summary>
    /// Registers a Camera in _cameras under the given name. Used in RegisterCamera Start. If the CameraManager have no Camera, the first registered will be the active one.
    /// The next ones will be disabled, as the AudioListener associated.
    /// </summary>
    /// <param name="c">The GameObject of the Camera to register.</param>
    /// <param name="name">The name to register the Camera under.</param>
    /// <returns>True if the name isn't already registered.</returns>
    public bool RegisterCamera(GameObject c, string name)
    {
        if (!_cameras.ContainsKey(name))
        {
            _cameras.Add(name, c);
            if (_active == null)
            {
                _active = c.GetComponent<Camera>();
                _active.enabled = true;
                c.GetComponent<AudioListener>().enabled = true;
                _activeName = name;
            }
            else
            {
                c.GetComponent<Camera>().enabled = false;
                c.GetComponent<AudioListener>().enabled = false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the Panel used to fade between two Cameras.
    /// </summary>
    /// <param name="screen">The script linked to the panel for the fading.</param>
    public void SetFadeScreen(RegisterFadeScreen screen)
    {
        _fadescreen = screen;
    }

    /// <summary>
    /// Turns the active Camera around a center, around the Y axe. If speed is positive, turns clockwise. If speed is negative, turns counterclockwise.
    /// </summary>
    /// <param name="center">The center of the rotation.</param>
    /// <param name="speed">The speed of the rotation.</param>
    public void AroundY(Vector3 center, float speed)
    {
        _active.transform.LookAt(center, Vector3.up);
        _active.transform.RotateAround(center, Vector3.up, speed * Time.timeScale);
    }

    /// <summary>
    /// Turns the active Camera's forward towards a GameObject position.
    /// </summary>
    /// <param name="go">The GameObject the Camera has to aim.</param>
    public void LookAt(GameObject go)
    {
        _active.transform.LookAt(go.transform);
    }

    public void MoveForward(float speed)
    {
        Transform t = _active.transform;
        MoveTo(t.position + t.forward, speed);
    }

    public void MoveBackward(float speed)
    {
        Transform t = _active.transform;
        MoveTo(t.position - t.forward, speed);
    }

    public void ZoomInPerspective(float times, float speed)
    {
        if(times < 0)
        {
            Logger.Warning("You should use ZoomOutPerspective instead (ZoomInPerspective with times < 0 ).");
        }
        if (!_active.GetComponent<Camera>().orthographic)
        {
            Transform t = _active.transform;
            MoveTo(t.position + t.forward* times, speed);
        }
        else
        {
            Logger.Warning("Using ZoomInPerspective with an orthographic Camera named " + _activeName + ".");
        }
    }

    public void ZoomOutPerspective(float times, float speed)
    {
        if (times < 0)
        {
            Logger.Warning("You should use ZoomInPerspective instead (ZoomOutPerspective with times < 0 ).");
        }
        if (!_active.GetComponent<Camera>().orthographic)
        {
            Transform t = _active.transform;
            MoveTo(t.position - t.forward * times, speed);
        }
        else
        {
            Logger.Warning("Using ZoomOutPerspective with an orthographic Camera named " + _activeName + ".");
        }
    }

    public void MoveTo(Vector3 pos, float speed)
    {
        _active.GetComponent<RegisterCamera>().MoveTo(pos, speed);
    }

    public void ZoomInOrthographic(float orthoSize, float speed)
    {
        float finalSize = _active.GetComponent<Camera>().orthographicSize - orthoSize;
        if(finalSize < 0)
        {
            Logger.Warning(_activeName + " is now reversed (Destination size : " + finalSize + " )");
        }
        _active.GetComponent<RegisterCamera>().ZoomOrthoTo(finalSize, speed);
    }

    public void ZoomOutOrthographic(float orthoSize, float speed)
    {
        float finalSize = _active.GetComponent<Camera>().orthographicSize + orthoSize;
        _active.GetComponent<RegisterCamera>().ZoomOrthoTo(finalSize, speed);
    }

    public void ZoomToOrthographic(float orthoSize, float speed)
    {
        if (orthoSize < 0)
        {
            Logger.Warning(_activeName + " is now reversed (Destination size : " + orthoSize + " )");
        }
        _active.GetComponent<RegisterCamera>().ZoomOrthoTo(orthoSize, speed);
    }

    public void Fov(float angle)
    {
        _active.fieldOfView = angle;
    }

    public bool StartAnimation()
    {
        Animator anim = _active.gameObject.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("Go", true);
            return true;
        }
        return false;
    }

    public void FadeTo(string cameraName)
    {
        if (IsStable())
        {
            if (_cameras.ContainsKey(cameraName))
            {
                _isStable = false;
                _fadescreen.Reverse();
                _nextCamera = cameraName;
                _fadescreen.FadeTime();
            }
            else
                Logger.Error("Does not exist : " + cameraName);
        }
        else
        {
            Logger.Warning("Camera already being changed.");
        }
    }

    public void ActivateMain()
    {
        _active.enabled = false;
        _active.gameObject.GetComponent<AudioListener>().enabled = false;
        _active = _cameras[_nextCamera].GetComponent<Camera>();
        _active.enabled = true;
        _active.gameObject.GetComponent<AudioListener>().enabled = true;
        _activeName = _nextCamera;
        _nextCamera = "";
        Logger.Trace("Has animation started ? " + StartAnimation());
        _isStable = true;
    }

    public bool IsStable()
    {
        return _isStable;
    }

}
