using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

/// <summary>
/// The class used to register AudioClips under a group name.
/// </summary>
public class AudioData : MonoBehaviour
{
    /// <summary>
    /// The name of the group of sounds.
    /// </summary>
    public string channel;
    /// <summary>
    /// The list containing the different AudioClips of the group.
    /// </summary>
    public List<AudioClip> clips;
    /// <summary>
    /// Are the AudioClips to be played in a random order ? If not, sequentially.
    /// </summary>
    public bool random;
    /// <summary>
    /// The index of the first AudioClip to be played if sequentially.
    /// </summary>
    private int seq = 0;
    /// <summary>
    /// The priority of the AudioClips.
    /// </summary>
    public int priority;
    /// <summary>
    /// Minimum pitch used to play the group.
    /// </summary>
    public float pitchMin;
    /// <summary>
    /// Maximal pitch used to play the group.
    /// </summary>
    public float pitchMax;
    /// <summary>
    /// The AudioMixerGroup the AudioClip are controled by.
    /// </summary>
    public AudioMixerGroup group;

    void Awake()
    {
        AudioManager.GetInstance().Register(channel, this);
    }

    void OnDestroy()
    {
        AudioManager.GetInstance().Withdraw(channel);
    }

    /// <summary>
    /// Returns an AucioClip, based on whether it is random ou sequential.
    /// </summary>
    /// <returns>The AudioClip selected.</returns>
    public AudioClip GetClip()
    {
        AudioClip clip;
        if (random)
        {
            int id = EruleRandom.RangeValue(0, clips.Count - 1);
            clip = clips[id];
        }
        else
            clip = clips[seq];

        seq = (seq + 1) % clips.Count;
        return clip;
    }

}
