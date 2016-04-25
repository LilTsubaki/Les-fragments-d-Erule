using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using System.IO;

public class TitleScriptClient : MonoBehaviour
{
    public Animator Title_Logo;
    public Animator Push;
    public List<Animator> animators;
    public ParticleSystem Particle_Energy;
    public ParticleSystem Particle_Title;
    public GameObject menu;
    public GameObject planks;
    public GameObject deckSelection;
    public GameObject gameCamera;

    private bool _logoFinish = false;

    public FloatingObjects _logo;

    private int _eruleVoiceId;
    private int _idLogoSound;

    private string _chosenMap;
    private string _chosenEnvironment;
    private string _cameraAnimation;

    void Start()
    {
        _eruleVoiceId = AudioManager.GetInstance().Play("EruleVoice");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!_logoFinish)
            {
                Title_Logo.SetTrigger("play");
                TitleDisappear();

                AudioManager.GetInstance().FadeOut(_eruleVoiceId);
                _idLogoSound = AudioManager.GetInstance().Play("Logo", true, false);
                float logoSoundLength = AudioManager.GetInstance().GetPlayerDuration(_idLogoSound);
                _logoFinish = true;
            }
            else
            {
                
                UIManager.GetInstance().ShowPanelNoStack("PanelRunicBoard");
                deckSelection.SetActive(true);
                planks.SetActive(true);
                menu.SetActive(false);
                Camera.main.gameObject.SetActive(false);
                AudioManager.GetInstance().StopPlayer(_idLogoSound);
                gameCamera.SetActive(true);
            }
                
        }        
    }

    private void SetFloatingLogo()
    {
        _logo.enabled = true;
    }

    private void TitleDisappear()
    {
        Particle_Title.Play();
        Push.SetTrigger("play");
        Invoke("EnergyBall", 3.5f);
    }

    private void EnergyBall()
    {
        Particle_Energy.Play();
        Particle_Energy.gameObject.GetComponent<Animator>().enabled = true;
        Invoke("BuildLogo", 4.3f);
    }

    private void BuildLogo()
    {
        Particle_Energy.Stop();
        foreach (var a in animators)
        {
            a.SetTrigger("play");
        }

        Invoke("SetFloatingLogo", 3f);
    }




}
