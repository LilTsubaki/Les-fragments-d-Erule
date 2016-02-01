using UnityEngine;

/// <summary>
/// The class used in order to apply effects when playing a sound.
/// </summary>
public class AudioPlayer : MonoBehaviour {

    /// <summary>
    /// The AudioSource to play.
    /// </summary>
    public AudioSource _audio;
    /// <summary>
    /// The power of the voulme of the sound [0,1].
    /// </summary>
    public float _power;
    /// <summary>
    /// Is the sound being played in ?
    /// </summary>
    public bool _fadeIn;
    /// <summary>
    /// Is the sound being played out ?
    /// </summary>
    public bool _fadeOut;
    
	// Use this for initialization
	void Start () {
        _power = 1;
	}
	
	// Update is called once per frame
	void Update () {

        // If faded in, we raise the volume of the sound up to 1. When at 1, we disable the behaviour.
        if (_fadeIn)
        {
            _power = Mathf.Min(1, _power + Time.deltaTime);
            if(_power == 1)
            {
                _fadeIn = false;
            }
        }
        // If faded out, we lower the volume of the sound down to 0. When at 0, we disable the behaviour.
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

    /// <summary>
    /// Enables the fading in behaviour in Update and disables the fading out.
    /// </summary>
    public void FadeIn()
    {
        _fadeOut = false;
        _fadeIn = true;
    }

    /// <summary>
    /// Enables the fading out behaviour in Update and disables the fading in.
    /// </summary>
    public void FadeOut()
    {
        _fadeOut = true;
        _fadeIn = false;
    }
}
