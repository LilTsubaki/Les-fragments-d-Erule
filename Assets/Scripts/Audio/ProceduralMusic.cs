using UnityEngine;
using System.Collections.Generic;

public class ProceduralMusic : MonoBehaviour {

    public int _minPlaying;
    public int _maxPlaying;

    public List<AudioData> _audios;
    private Dictionary<string, int> _players;
    public int _nbPlaying = 0;
    private bool _playing;

    [Header("Strategy - Wait Time")]
    public float _waitTimeMin;
    public float _waitTimeMax;
    private float _waitTime;
    public int _nbPlayersToChange;
    private float _remainingTimeForWait;

    public bool _initiated;

    // Use this for initialization
    void Start () {
        _players = new Dictionary<string, int>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_playing)
        {
            _remainingTimeForWait += Time.deltaTime;
            if (_remainingTimeForWait >= _waitTime)
            {
                for (int i = 0; i < _nbPlayersToChange; ++i)
                {
                    int rand = EruleRandom.RangeValue(0, _players.Count - 1);
                    int id = _players[_audios[rand].channel];
                    float power = AudioManager.GetInstance().GetPlayerPower(id);
                    Logger.Debug(id);
                    if (power == 0)
                    {
                        if (_nbPlaying < _maxPlaying)
                        {
                            AudioManager.GetInstance().FadeIn(id);
                            float randPos = EruleRandom.RangeValue(-1f, 1f);
                            AudioManager.GetInstance().FadePanoramicStereo(id, randPos);
                            ++_nbPlaying;
                        }
                    }
                    else if(power == 1)
                    {
                        if (_nbPlaying > _minPlaying)
                        {
                            AudioManager.GetInstance().FadeOut(id);
                            --_nbPlaying;
                        }
                    }
                }
                _remainingTimeForWait = 0;
                _waitTime = EruleRandom.RangeValue(_waitTimeMin, _waitTimeMax);
            }
        }
	
	}

    public void InitPlayers()
    {
        if (_initiated)
            return;

        for(int i = 0; i < _audios.Count; ++i)
        {
            string channel = _audios[i].channel;
            int id = AudioManager.GetInstance().PlayLoopingClips(channel, true, false);
            _players.Add(channel, id);
            AudioManager.GetInstance().SetPlayerPower(id, 0);
        }

        _initiated = true;
    }

    public void StartPlaying()
    {
        InitPlayers();

        int nbPlayers = _minPlaying;
        if(nbPlayers < 1)
        {
            Logger.Warning("Insufficient players number for the music to start : " + nbPlayers);
            return;
        }
        if (nbPlayers > _players.Count)
            nbPlayers = _players.Count;

        List<int> ids = new List<int>();
        for (int i = 0; i < nbPlayers; ++i)
        {
            int randPlayerId = EruleRandom.RangeValue(0, _players.Count-1);
            while (ids.Contains(randPlayerId))
            {
                randPlayerId = EruleRandom.RangeValue(0, _players.Count-1);
            }
            string chan = _audios[randPlayerId].channel;
            AudioManager.GetInstance().FadeIn(_players[chan]);
            ++_nbPlaying;
            ids.Add(randPlayerId);
        }
        _waitTime = EruleRandom.RangeValue(_waitTimeMin, _waitTimeMax);
        _playing = true;
    }

    public void Stop()
    {
        _playing = false;
        foreach(int id in _players.Values)
        {
            AudioManager.GetInstance().StopPlayLoopingClips(id);
        }
        _players.Clear();
        _nbPlaying = 0;
        _initiated = false;
    }
}
