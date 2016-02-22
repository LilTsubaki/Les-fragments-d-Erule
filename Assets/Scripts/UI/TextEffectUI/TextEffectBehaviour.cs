using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextEffectBehaviour : MonoBehaviour {

    private float _currentTime;
    public float _duration;
    public float _fadeOutStart;

    internal bool _hasAnImage;

    private Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.position;
    }
    
	
	// Update is called once per frame
	void Update () {
        _currentTime += Time.deltaTime;
        Text text = gameObject.GetComponentInChildren<Text>();
        Image image = gameObject.GetComponentInChildren<Image>();
        if (_currentTime < _duration)
        {
            float step = Time.deltaTime * 2;
            transform.position = Vector3.Slerp(transform.position, _initialPosition + new Vector3(0, 1, 0), step);
            if (_currentTime > _fadeOutStart)
            {
                float newAlpha = 1 - (_currentTime - _fadeOutStart) / (_duration - _fadeOutStart);
                text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
                if (_hasAnImage)
                    image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            } 
        }
        else
        {
            _currentTime = 0;
            gameObject.transform.position = _initialPosition;
            if (_hasAnImage)
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            gameObject.SetActive(false);
        }
	}
}
