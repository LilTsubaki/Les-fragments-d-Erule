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
    /// <summary>
    /// Does the same clip has to be played looping ?
    /// </summary>
    public bool _loopClips;
    /// <summary>
    /// The channel of AudioData to be played looping .
    /// </summary>
    public string _channelName;
    public bool _randPitch;
    public float _minPitch;
    public float _maxPitch;
    private AudioManager.spatialization _space;
    private Vector3 _pos;
    private float _distMin;
    private float _distMax;
    private int _allocatedId;

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

        if (_loopClips)
        {
            if (!_audio.isPlaying)
            {
                Logger.Debug("Boop " + _allocatedId);
                AudioManager.GetInstance().PlayOnPlayer(_allocatedId, true, _channelName, _randPitch, _space, _pos, _distMin, _distMax);
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

    public void Looping(int selfId, string channel, bool randPitch, AudioManager.spatialization space, Vector3 pos, float distMin, float distMax)
    {
        _loopClips = true;
        _channelName = channel;
        _randPitch = randPitch;
        _space = space;
        _pos = pos;
        _distMin = distMin;
        _distMax = distMax;
        _allocatedId = selfId;
    }

    public void StopLooping()
    {
        _loopClips = false;
        _channelName = "";
        _randPitch = false;
        _pos = Vector3.zero;
        _distMin = 0;
        _distMax = 0;
    }
}
