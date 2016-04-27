using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayVideo : MonoBehaviour
{
    MovieTexture mt;
    private bool _videoStarted;

    int _video;

    void Awake()
    {
        RawImage rim = GetComponent<RawImage>();
        mt = (MovieTexture)rim.mainTexture;
        _videoStarted = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && _videoStarted)
        {
            AudioManager.GetInstance().StopPlayer(_video);
            SceneManager.LoadScene("Scenes/Tests/Clement/Menu");
        }

        if (Input.GetMouseButtonUp(0) && !_videoStarted)
        {
            Logger.Debug("play");
            mt.Play();
            _video = AudioManager.GetInstance().Play("trailer");
            _videoStarted = true;
        }

    }
}