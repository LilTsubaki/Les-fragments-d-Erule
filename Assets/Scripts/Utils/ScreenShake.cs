using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

    float _elapsed = 0.0f;
    public float _duration;
    public float _magnitude;
    public bool _shaking;
    private bool _positionRegistered = false;
    Vector3 originalCamPos;

    // Use this for initialization
    void Start ()
    {
	}

    public void Shake(float duration, float magnitude)
    {
        _duration = duration;
        _magnitude = magnitude;
        _shaking = true;
    }

    public void ResetCamera()
    {
        _elapsed = 0.0f;
        _shaking = false;
        _positionRegistered = false;
        Camera.main.transform.position = originalCamPos;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_shaking)
        {
            if (!_positionRegistered)
            {
                originalCamPos = Camera.main.transform.position;
                _positionRegistered = true;
            }
            _elapsed += Time.deltaTime;
            if (_elapsed <= _duration)
            {
                float percentComplete = _elapsed / _duration;
                float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

                // map value to [-1, 1]
                float x = Random.value * 2.0f - 1.0f;
                float y = Random.value * 2.0f - 1.0f;
                x *= _magnitude * damper;
                y *= _magnitude * damper;

                Camera.main.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);
            }
            else
            {
                ResetCamera();
            }
        }
    }
}
