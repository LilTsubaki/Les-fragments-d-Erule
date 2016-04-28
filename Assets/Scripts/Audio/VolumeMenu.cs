using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class VolumeMenu : MonoBehaviour {

    public AudioMixer _mixer;

	public void SetMasterLvl(float lvl)
    {
        _mixer.SetFloat("General", lvl);
    }

    public void SetMusicLvl(float lvl)
    {
        _mixer.SetFloat("Music", lvl);
    }

    public void SetInterfaceLvl(float lvl)
    {
        _mixer.SetFloat("Interface", lvl);
    }

    public void SetSpellEffectsLvl(float lvl)
    {
        _mixer.SetFloat("SpellEffects", lvl);
    }

    public void SetVoiceLvl(float lvl)
    {
        _mixer.SetFloat("Voice", lvl);
    }

    public void SetEnvironmentLvl(float lvl)
    {
        _mixer.SetFloat("Environment", lvl);
    }
}
