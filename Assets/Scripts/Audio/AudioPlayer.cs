using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour {

    public AudioSource _audio;
    public float _power;
    public bool _fadeIn;
    public bool _fadeOut;
    
	// Use this for initialization
	void Start () {
        _power = 1;
	}
	
	// Update is called once per frame
	void Update () {

        if (_fadeIn)
        {
            _power = Mathf.Min(1, _power + Time.deltaTime);
            if(_power == 1)
            {
                _fadeIn = false;
            }
        }
        else if (_fadeOut)
        {
            _power = Mathf.Max(0, _power - Time.deltaTime);
            if (_power == 0)
            {
                _fadeOut = false;
            }
        }

        _audio.volume = _power;
    }

    public void FadeIn()
    {
        _fadeOut = false;
        _fadeIn = true;
        _power = 0;
    }

    public void FadeOut()
    {
        _fadeOut = true;
        _fadeIn = false;
    }
}
