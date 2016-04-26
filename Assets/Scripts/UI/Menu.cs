using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using System.IO;

public class Menu : MonoBehaviour
{
    public Animator Title_Logo;
    public Animator Push;
    public List<Animator> animators;
    public ParticleSystem Particle_Energy;
    public ParticleSystem Particle_Title;

    public GameObject server;
    public GameObject menu;

    private bool _logoFinish = false;

    public FloatingObjects _logo;

    private int _eruleVoiceId;
    private int logoId;
    private int _musicId;

    private string _chosenMap;
    private string _chosenEnvironment;
    private string _cameraAnimation;

    public Button buttonMap1;
    public Button buttonMap2;

    public GameObject _player1GameObject;
    public GameObject _player2GameObject;
    public GameObject _buttonValidation;

    public CharacterHostUI _uiPlayer1;
    public CharacterHostUI _uiPlayer2;

    public AroundMapScreen _around;
    public Text _textPlayerToPlace;



    void Start()
    {
        _eruleVoiceId = AudioManager.GetInstance().Play("EruleVoice");
        buttonMap1.onClick.AddListener(delegate { AudioManager.GetInstance().Play("choixMap"); LoadingScreen("iles_englouties", "Asset_Iles_englouties", "Iles"); });
        buttonMap2.onClick.AddListener(delegate { AudioManager.GetInstance().Play("choixMap"); LoadingScreen("sentiersGeles", "Asset_Sentiers_Geles", "Sentiers"); });

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && !_logoFinish)
        {
            Title_Logo.SetTrigger("play");
            TitleDisappear();

            AudioManager.GetInstance().FadeOut(_eruleVoiceId);
            int idLogoSound = AudioManager.GetInstance().Play("Logo", true, false);
            float logoSoundLength = AudioManager.GetInstance().GetPlayerDuration(idLogoSound);

            server.SetActive(true);

            Invoke("StartMenuMusic", logoSoundLength);

            _logoFinish = true;
        }
        ServerManager manager = ServerManager.GetInstance();
        if (manager._server != null && !manager._server.SearchingClient)
        {
            UIManager.GetInstance().ShowPanelNoStack("PanelChoiceMap");
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
        foreach(var a in animators)
        {
            a.SetTrigger("play");
        }

        Invoke("SetFloatingLogo", 3f);
    }

    private void StartMenuMusic()
    {
        _musicId = AudioManager.GetInstance().PlayLoopingClips("MusicMenu");
    }

    void LoadingScreen(string path, string environment, string animation)
    {
        AudioManager.GetInstance().StopPlayLoopingClips(_musicId);
        UIManager.GetInstance().FadeOutPanelNoStack("PanelChoiceMap");
        UIManager.GetInstance().FadeInPanelNoStack("Loading");
        _chosenMap = path;
        _chosenEnvironment = environment;
        _cameraAnimation = animation;
        Invoke("LoadMap", 1);
    }

    void LoadMap()
    {        
        string path = _chosenMap;
        GameObject o = new GameObject();
        string name = Path.GetFileNameWithoutExtension(path);
        SpawnAndGameBehaviour tsn = o.AddComponent<SpawnAndGameBehaviour>();
        tsn._button = _buttonValidation;
        tsn._button.GetComponent<Button>().onClick.AddListener(delegate { tsn.changeState(); });
        tsn._player1GameObject = _player1GameObject;
        tsn._player2GameObject = _player2GameObject;
        tsn._uiPlayer1 = _uiPlayer1;
        tsn._uiPlayer2 = _uiPlayer2;
        tsn._boardName = name;
        tsn._prefabEnvironmentName = _chosenEnvironment;
        _around.map = tsn;
        tsn._textPlayerToPlace = _textPlayerToPlace;
        CameraManager.GetInstance().Active.GetComponent<Animator>().SetTrigger(_cameraAnimation);
        CameraManager.GetInstance().Active.fieldOfView = 25;
        menu.SetActive(false);
    }

}
