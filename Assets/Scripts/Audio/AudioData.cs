using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioData : MonoBehaviour
{

    public string channel;
    public List<AudioClip> clips;
    public bool random;
    private int seq = 0;
    public int priority;
    public float pitchMin;
    public float pitchMax;
    public AudioMixerGroup group;

    void Awake()
    {
        AudioManager.GetInstance().Register(channel, this);
    }

    void OnDestroy()
    {
        AudioManager.GetInstance().Withdraw(channel);
    }

    public AudioClip GetClip()
    {
        AudioClip clip;
        if (random)
        {
            int id = Random.Range(0, clips.Count - 1);
            clip = clips[id];
        }
        clip = clips[seq];

        seq = (seq + 1) % clips.Count;
        return clip;
    }

}
