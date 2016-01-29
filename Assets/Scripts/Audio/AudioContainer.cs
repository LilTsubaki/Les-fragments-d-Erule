using UnityEngine;
using System.Collections;

public class AudioContainer : Poolable<AudioContainer>
{

    internal GameObject _audioGameObject;

    public void Copy(AudioContainer t)
    {
        _audioGameObject = UnityEngine.Object.Instantiate(t._audioGameObject);
    }

    public bool IsReady()
    {
        return !_audioGameObject.GetComponent<AudioPlayer>()._audio.isPlaying;
    }

    public void Pick()
    {
        AudioPlayer ap = _audioGameObject.GetComponent<AudioPlayer>();
        ap._power = 1;
        ap._fadeIn = false;
        ap._fadeOut = false;
    }
}