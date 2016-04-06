using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The class managing the Cameras. Cameras have to be registered with the script RegisterCamera.
/// </summary>
public class CameraManager : Manager<CameraManager>
{
    /// <summary>
    /// The mapping between cameras' names and the Game Objects of the registered Cameras.
    /// </summary>
    private Dictionary<string, GameObject> _cameras;
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

    public Camera Active
    {
        get
        {
            return _active;
        }
    }

    /// <summary>
    /// Constructor of CameraManager. Must be used in GetInstance only.
    /// </summary>
    public CameraManager()
    {
        if (_instance != null)
            throw new ManagerException();
        _cameras = new Dictionary<string, GameObject>();
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
    /// Turns the active Camera around a center, around the Y axis. If speed is positive, turns clockwise. If speed is negative, turns counterclockwise.
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

    /// <summary>
    /// Makes the active Camera move once along its forward direction.
    /// </summary>
    /// <param name="transitionTime">The time in seconds to finish the movement.</param>
    public void MoveForward(float transitionTime)
    {
        Transform t = _active.transform;
        MoveTo(t.position + t.forward, transitionTime);
    }

    /// <summary>
    /// Makes the active Camera move once along its backward direction.
    /// </summary>
    /// <param name="transitionTime">The time in seconds to finish the movement.</param>
    public void MoveBackward(float transitionTime)
    {
        Transform t = _active.transform;
        MoveTo(t.position - t.forward, transitionTime);
    }

    /// <summary>
    /// Zooms the active Camera in if its projection is in perspective by moving it along its forward direction.
    /// </summary>
    /// <param name="times">The number of times to move the Camera.</param>
    /// <param name="transitonTime">The time in seconds to finish the zoom. If 0, changed to 0.0001.</param>
    public void ZoomInPerspective(float times, float transitonTime)
    {
        if (transitonTime == 0)
        {
            transitonTime = 0.0001f;
        }
        if (times < 0)
        {
            Logger.Warning("You should use ZoomOutPerspective instead (ZoomInPerspective with times < 0 ).");
        }
        if (!_active.GetComponent<Camera>().orthographic)
        {
            Transform t = _active.transform;
            MoveTo(t.position + t.forward* times, transitonTime);
        }
        else
        {
            Logger.Warning("Using ZoomInPerspective with an orthographic Camera named " + _activeName + ".");
        }
    }

    /// <summary>
    /// Zooms the active Camera out if its projection is in perspective by moving it along its backward direction.
    /// </summary>
    /// <param name="times">The number of times to move the Camera.</param>
    /// <param name="transtionTime">The time in seconds to finish the zoom. If 0, changed to 0.0001.</param>
    public void ZoomOutPerspective(float times, float transtionTime)
    {
        if(transtionTime == 0)
        {
            transtionTime = 0.0001f;
        }
        if (times < 0)
        {
            Logger.Warning("You should use ZoomInPerspective instead (ZoomOutPerspective with times < 0 ).");
        }
        if (!_active.GetComponent<Camera>().orthographic)
        {
            Transform t = _active.transform;
            MoveTo(t.position - t.forward * times, transtionTime);
        }
        else
        {
            Logger.Warning("Using ZoomOutPerspective with an orthographic Camera named " + _activeName + ".");
        }
    }

    /// <summary>
    /// Moves the active Camera to the specified position.
    /// </summary>
    /// <param name="pos">The position to move the active Camera to.</param>
    /// <param name="transitionTime">The time in seconds to finish the movement.</param>
    public void MoveTo(Vector3 pos, float transitionTime)
    {
        _active.GetComponent<RegisterCamera>().MoveTo(pos, transitionTime);
    }

    /// <summary>
    /// Zooms the active Camera in by raising its orthographic size by a certain amount.
    /// </summary>
    /// <param name="orthoSize">The amount to increase the size.</param>
    /// <param name="transitionTime">The time in seconds to finish the zoom.</param>
    public void ZoomInOrthographic(float orthoSize, float transitionTime)
    {
        if(transitionTime == 0)
        {
            transitionTime = 0.0001f;
        }
        if(transitionTime < 0)
        {
            Logger.Warning("Trying to use ZoomInOrthographic with a negative transition time.");
        }
        if (_active.GetComponent<Camera>().orthographic)
        {
            float finalSize = _active.GetComponent<Camera>().orthographicSize - orthoSize;
            ZoomToOrthographic(finalSize, transitionTime);
        }
        else
        {
            Logger.Warning("Using ZoomInOrthographic with a perspective Camera named " + _activeName + ".");
        }
    }

    /// <summary>
    /// Zooms the active Camera out by lowering its orthographic size by a certain amount.
    /// </summary>
    /// <param name="orthoSize">The amount to decrease the size.</param>
    /// <param name="transitionTime">The time in seconds to finish the zoom.</param>
    public void ZoomOutOrthographic(float orthoSize, float transitionTime)
    {
        if (transitionTime == 0)
        {
            transitionTime = 0.0001f;
        }
        if (transitionTime < 0)
        {
            Logger.Warning("Trying to use ZoomOutOrthographic with a negative transition time.");
        }
        if (_active.GetComponent<Camera>().orthographic)
        {
            float finalSize = _active.GetComponent<Camera>().orthographicSize + orthoSize;
            ZoomToOrthographic(finalSize, transitionTime);
        }
        else
        {
            Logger.Warning("Using ZoomOutOrthographic with a perspective Camera named " + _activeName + ".");
        }
    }

    /// <summary>
    /// Zooms the active Camera by changing its orthographic size to a new value.
    /// </summary>
    /// <param name="orthoSize">The new value of the orthographic size.</param>
    /// <param name="transitionTime">The time in seconds to finish the zoom.</param>
    public void ZoomToOrthographic(float orthoSize, float transitionTime)
    {
        if (_active.GetComponent<Camera>().orthographic)
        {
            if (orthoSize < 0)
            {
                Logger.Warning(_activeName + " will be reversed (Destination size : " + orthoSize + " )");
            }
            _active.GetComponent<RegisterCamera>().ZoomOrthoTo(orthoSize, transitionTime);
        }
        else
        {
            Logger.Warning("Using ZoomToOrthographic with a perspective Camera named " + _activeName + ".");
        }
    }

    /// <summary>
    /// Changes the field of view of the active Camera to a new value.
    /// </summary>
    /// <param name="angle">The new field of view angle of the active Camera.</param>
    public void Fov(float angle)
    {
        _active.fieldOfView = angle;
    }

    /// <summary>
    /// Activates a trigger in the animator on the active Camera.
    /// </summary>
    /// <param name="animatorTriggerName">The trigger to activate.</param>
    /// <returns>True if the active Camera has an animator. Else false.</returns>
    public bool StartAnimation(string animatorTriggerName)
    {
        Animator anim = _active.gameObject.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger(animatorTriggerName);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Fades to another Camera if there is not any other fading effect being played.
    /// </summary>
    /// <param name="cameraName">The name of the Camera to fade to.</param>
    /// <param name="transitionTime">The duration of the transition, in seconds.</param>
    public void FadeTo(string cameraName, float transitionTime)
    {
        if (IsStable())
        {
            if (_cameras.ContainsKey(cameraName))
            {
                _isStable = false;
                _nextCamera = cameraName;
                if (_fadescreen != null)
                {
                    _fadescreen.Reverse(transitionTime);
                    _fadescreen.FadeTime(transitionTime * 0.5f);
                }
                else
                {
                    ActivateMain();
                }
            }
            else
                Logger.Error("Does not exist : " + cameraName);
        }
        else
        {
            Logger.Warning("Camera already being changed.");
        }
    }

    /// <summary>
    /// Fade to another Camera if there is not any other fading effect beign played. Activates a trigger on the animator of the next Camera.
    /// </summary>
    /// <param name="cameraName">The name of the Camera to fade to.</param>
    /// <param name="transitionTime">The duration of the transition, in seconds.</param>
    /// <param name="animTriggerName">The name of the trigger to activate.</param>
    public void FadeToAnim(string cameraName, float transitionTime, string animTriggerName)
    {
        if (IsStable())
        {
            if (_cameras.ContainsKey(cameraName))
            {
                _isStable = false;
                _fadescreen.Reverse(transitionTime);
                _fadescreen.SetAnimation(animTriggerName);
                _nextCamera = cameraName;
                _fadescreen.FadeTimeAnim(transitionTime*0.5f);
            }
            else
                Logger.Error("Does not exist : " + cameraName);
        }
        else
        {
            Logger.Warning("Camera already being changed.");
        }
    }

    /// <summary>
    /// Switches to the Camera marked as next.
    /// </summary>
    public void ActivateMain()
    {
        _active.enabled = false;
        _active.gameObject.GetComponent<AudioListener>().enabled = false;
        _active = _cameras[_nextCamera].GetComponent<Camera>();
        _active.enabled = true;
        _active.gameObject.GetComponent<AudioListener>().enabled = true;
        _activeName = _nextCamera;
        _nextCamera = "";
        _isStable = true;
    }


    /// <summary>
    /// Switches to the Camera marked as next and activates the given trigger.
    /// </summary>
    /// <param name="anim">The name of the trigger to activate.</param>
    public void ActivateMainStartAnim(string anim)
    {
        ActivateMain();
        Logger.Trace("Has animation started ? " + StartAnimation(anim));
    }

    /// <summary>
    /// Returns if there is not any transition between two Cameras being played.
    /// </summary>
    /// <returns>True if there is not any transition being played.</returns>
    public bool IsStable()
    {
        return _isStable;
    }

}
