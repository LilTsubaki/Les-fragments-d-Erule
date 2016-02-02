using UnityEngine;

public class RegisterCamera : MonoBehaviour
{

    /// <summary>
    /// The name to register the Camera under.
    /// </summary>
    public string _cameraName;
    /// <summary>
    /// The registered Camera.
    /// </summary>
    Camera _camera;

    // Zoom values
    /// <summary>
    /// Is there a transition being played on the orthographic size ?
    /// </summary>
    bool _isChangingOrthoSize;
    /// <summary>
    /// The time the transition started.
    /// </summary>
    float _startingChangeTime;
    /// <summary>
    /// The orthographic size the Camera is changing to.
    /// </summary>
    float _orthographicSize;
    /// <summary>
    /// The starting orthographic size, before a transition.
    /// </summary>
    float _orthographicSizePrevious;
    /// <summary>
    /// The time of the zooming transition.
    /// </summary>
    float _speedZoom;

    // Movement values
    /// <summary>
    /// Is there a transition being played on the Camera position ?
    /// </summary>
    bool _isMoving;
    /// <summary>
    /// The destination of the Camera.
    /// </summary>
    Vector3 _posAimed;
    /// <summary>
    /// The time of the moving transition.
    /// </summary>
    float _speedMove;

    void Start()
    {
        CameraManager.GetInstance().RegisterCamera(gameObject, _cameraName);
        _camera = gameObject.GetComponent<Camera>();
        _posAimed = gameObject.transform.position;
        if (_camera.orthographic)
        {
            _orthographicSize = _camera.orthographicSize;
            _orthographicSizePrevious = _orthographicSize;
        }
    }

    void Update()
    {
        CheckMove();
        CheckZoom();
    }

    /// <summary>
    /// Checks if the Camera has to be moved and changes its position if needed.
    /// </summary>
    void CheckMove()
    {
        if(_isMoving){
            Vector3 p = Vector3.MoveTowards(gameObject.transform.position, _posAimed, Time.deltaTime * _speedMove);
            gameObject.transform.position = p;
            _isMoving = !p.Equals(_posAimed);
        }
    }

    /// <summary>
    /// Checks if the Camera has to be zoomed and changes its orthographic size if needed.
    /// </summary>
    void CheckZoom()
    {
        if (_isChangingOrthoSize)
        {
            float newSize = Mathf.Lerp(_orthographicSizePrevious, _orthographicSize, (Time.time - _startingChangeTime) * _speedZoom);
            if(_camera.orthographicSize >= 0 && newSize < 0)
            {
                Logger.Warning("The Camera " + _cameraName + " is now reversed.");
            }
            _camera.orthographicSize = newSize;
            if (_camera.orthographicSize.Equals(_orthographicSize))
            {
                _orthographicSizePrevious = _orthographicSize;
                _isChangingOrthoSize = false;
            }
        }
    }

    /// <summary>
    /// Changes the state of the Camera in order to make it move.
    /// </summary>
    /// <param name="pos">The destination of the Camera.</param>
    /// <param name="speed">The duration of the movement, in seconds.</param>
    public void MoveTo(Vector3 pos, float speed)
    {
        _isMoving = true;
        _posAimed = pos;
        _speedMove = 1/speed;
    }

    /// <summary>
    /// Changes the state of the Camera in order to make it zoom. Only with orthographic Cameras.
    /// </summary>
    /// <param name="orthoSize">The wanted final orthographic size.</param>
    /// <param name="speed">The duration of the zoom, in seconds.</param>
    public void ZoomOrthoTo(float orthoSize, float speed)
    {
        _isChangingOrthoSize = true;
        _startingChangeTime = Time.time;
        _orthographicSize = orthoSize;
        _orthographicSizePrevious = _camera.orthographicSize;
        _speedZoom = 1/speed;
    }
}
