using UnityEngine;
using System.Collections;

public class RegisterCamera : MonoBehaviour
{

    public string _cameraName;
    Camera _camera;

    // Zoom values
    bool _isChangingOrthoSize;
    float _startingChangeTime;
    float _orthographicSize;
    float _orthographicSizePrevious;
    float _speedZoom;

    // Movement values
    bool _isMoving;
    Vector3 _posAimed;
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

    void CheckMove()
    {
        if(_isMoving){
            Vector3 p = Vector3.MoveTowards(gameObject.transform.position, _posAimed, _speedMove);
            gameObject.transform.position = p;
            _isMoving = !p.Equals(_posAimed);
        }
    }

    void CheckZoom()
    {
        if (_isChangingOrthoSize)
        {
            _camera.orthographicSize = Mathf.Lerp(_orthographicSizePrevious, _orthographicSize, (Time.time - _startingChangeTime) * _speedZoom);
            if (_camera.orthographicSize.Equals(_orthographicSize))
            {
                _orthographicSizePrevious = _orthographicSize;
                _isChangingOrthoSize = false;
            }
        }
    }


    public void MoveTo(Vector3 pos, float speed)
    {
        _isMoving = true;
        _posAimed = pos;
        _speedMove = speed;
    }

    public void ZoomOrthoTo(float orthoSize, float speed)
    {
        _isChangingOrthoSize = true;
        _startingChangeTime = Time.time;
        _orthographicSize = orthoSize;
        _orthographicSizePrevious = _camera.orthographicSize;
        _speedZoom = speed;
    }
}
