using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{

    public List<Animator> animators;

    public GameObject _logo;

    private bool _logoFinish = false;
    private int _eruleVoiceId;
    private int logoId; 

    void Start()
    {
        _eruleVoiceId = AudioManager.GetInstance().Play("EruleVoice");

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !_logoFinish)
        {
           foreach(var a in animators)
            {
                a.SetTrigger("play");
            }

            Invoke("SetFloatingLogo", 2.3f);

            AudioManager.GetInstance().FadeOut(_eruleVoiceId);
            int idLogoSound = AudioManager.GetInstance().Play("Logo");
            float logoSoundLength = AudioManager.GetInstance().GetPlayerDuration(idLogoSound);

            Invoke("StartMenuMusic", logoSoundLength);
            
            _logoFinish = true;
        }
    }

    private void SetFloatingLogo()
    {
        _logo.GetComponent<FloatingObjects>().enabled = true;
    }

    private void StartMenuMusic()
    {
        AudioManager.GetInstance().PlayLoopingClips("MusicMenu");
    }

}
