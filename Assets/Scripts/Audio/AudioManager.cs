using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager
{

    public enum spatialization : int { AUDIO_2D = 0, AUDIO_3D = 1 };


    private Dictionary<string, AudioData> _sounds;
    private List<AudioPlayer> _audiosources;
    static private AudioManager _instance;
    private bool _isMuted;

    private AudioManager()
    {
        _audiosources = new List<AudioPlayer>();
        _sounds = new Dictionary<string, AudioData>();
        GameObject parent = new GameObject();
        for (int i = 0; i < 50; ++i)
        {
            GameObject obj = new GameObject();
            AudioSource asource = obj.AddComponent<AudioSource>();
            AudioPlayer ap = obj.AddComponent<AudioPlayer>();
            ap._audio = asource;
            _audiosources.Add(ap);
            obj.transform.name = "Audiosource";
            obj.transform.parent = parent.transform;
        }

        _isMuted = false;
    }

    public static AudioManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new AudioManager();
        }
        return _instance;
    }

    public void Mute(bool m)
    {
        foreach (AudioPlayer ap in _audiosources)
        {
            ap._audio.mute = m;
        }
        _isMuted = m;
    }

    public void Mute()
    {
        _isMuted = !_isMuted;
        foreach (AudioPlayer ap in _audiosources)
        {
            ap._audio.mute = _isMuted;
        }
    }

    public bool Register(string name, AudioData data)
    {
        if (_sounds.ContainsKey(name))
        {
            return false;
        }
        _sounds.Add(name, data);
        return true;
    }

    public bool Withdraw(string name)
    {
        if (!_sounds.ContainsKey(name))
        {
            return false;
        }
        _sounds.Remove(name);
        return true;
    }

    private int GetFreeAudioSourceId(int priority)
    {
        int least = int.MinValue;
        int leastId = -1;
        AudioSource auSo;
        for (int i = 0; i < _audiosources.Count; ++i)
        {
            auSo = _audiosources[i].GetComponent<AudioSource>();
            if (!auSo.isPlaying)
            {
                return i;
            }
            if (auSo.priority >= least && auSo.priority >= priority)
            {
                least = auSo.priority;
                leastId = i;
            }
        }
        if (leastId != -1)
        {
            auSo = _audiosources[leastId].GetComponent<AudioSource>();
            auSo.Stop();
            return leastId;
        }
        return -1;
    }

    private AudioSource GetFreeAudioSource(int id)
    {
        return _audiosources[id].GetComponent<AudioSource>();
    }

    public int Play(string soundName)
    {
        return Play(soundName, false, spatialization.AUDIO_2D);
    }

    public int Play(string soundName, bool loop, spatialization space = spatialization.AUDIO_2D, float distMin = 0, float distMax = 0)
    {
        return Play(soundName, false, loop, space, distMin, distMax);
    }

    public int Play(string soundName, bool randPitch, bool loop, spatialization space = spatialization.AUDIO_2D, float distMin = 0, float distMax = 0)
    {
        return Play(soundName, randPitch, loop, space, Vector3.zero, distMin, distMax);
    }

    public int Play(string soundName, bool randPitch, bool loop, spatialization space, Vector3 pos, float distMin = 0, float distMax = 0)
    {
        if (_sounds.ContainsKey(soundName))
        {
            AudioData ad = _sounds[soundName];
            if (ad != null)
            {
                int id = GetFreeAudioSourceId(ad.priority);
                AudioSource auSo = GetFreeAudioSource(id);
                auSo.clip = ad.GetClip();
                auSo.outputAudioMixerGroup = ad.group;
                auSo.spatialBlend = (float)space;
                auSo.gameObject.transform.position = pos;
                auSo.minDistance = distMin;
                auSo.maxDistance = distMax;
                if (randPitch)
                {
                    auSo.pitch = Random.Range(ad.pitchMin, ad.pitchMax);
                }
                else
                {
                    auSo.pitch = 1;
                }
                auSo.Play();
                return id;
            }
        }
        return -1;
    }

    public int PlayFadeIn(string soundName)
    {
        return PlayFadeIn(soundName, false, spatialization.AUDIO_2D);
    }

    public int PlayFadeIn(string soundName, bool loop, spatialization space = spatialization.AUDIO_2D, float distMin = 0, float distMax = 0)
    {
        return PlayFadeIn(soundName, false, loop, space, distMin, distMax);
    }

    public int PlayFadeIn(string soundName, bool randPitch, bool loop, spatialization space = spatialization.AUDIO_2D, float distMin = 0, float distMax = 0)
    {
        return PlayFadeIn(soundName, randPitch, loop, space, Vector3.zero, distMin, distMax);
    }

    public int PlayFadeIn(string soundName, bool randPitch, bool loop, spatialization space, Vector3 pos, float distMin = 0, float distMax = 0)
    {
        int id = Play(soundName, randPitch, loop, space, pos, distMin, distMax);
        FadeIn(id);
        return id;
    }

    public bool FadeIn(int id)
    {
        if (id != -1 && id < _audiosources.Count)
            _audiosources[id].FadeIn();/*
        Si id compatible
            Fonction FadeIn du script AudioPlayer
        */
        return false;
    }

    public bool FadeOut(int id)
    {
        /*
        Si id compatible
            Fonction FadeOut du script AudioPlayer
        */
        if (id != -1 && id < _audiosources.Count)
            _audiosources[id].FadeOut();
        return false;
    }

    public void FreeResources()
    {
        foreach (AudioPlayer ap in _audiosources)
        {
            UnityEngine.Object.Destroy(ap.gameObject);
        }
    }

}
