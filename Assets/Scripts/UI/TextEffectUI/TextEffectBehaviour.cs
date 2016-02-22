using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextEffectBehaviour : MonoBehaviour {

    private float _currentTime;
    public float _duration;
    public float _fadeOutStart;

    internal bool _hasAnImage;

    private Vector3 _initialPosition;

    public Vector3 InitialPosition
    {
        get
        {
            return _initialPosition;
        }

        set
        {
            _initialPosition = value;
        }
    }

    void Start()
    {
        InitialPosition = transform.position;
    }
    
	
	// Update is called once per frame
	void Update () {
        _currentTime += Time.deltaTime;
        Text text = gameObject.GetComponentInChildren<Text>();
        Image image = gameObject.GetComponentInChildren<Image>();
        if (_currentTime < _duration)
        {
            float step = Time.deltaTime * 2;
            transform.position = Vector3.Slerp(transform.position, InitialPosition + Camera.main.transform.up*2, step);
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
            gameObject.transform.position = InitialPosition;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            gameObject.SetActive(false);
        }
	}
}
