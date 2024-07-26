using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    private const string MasterVolume = "Master";
    private const string MusicVolume = "Music";
    private const string SFXVolume = "SFX";

    public void OnMasterVolumeChanged(System.Single masterLevel)
    {
        SetMixerValue(MasterVolume, masterLevel);
    }
    
    public void OnMusicVolumeChanged(System.Single musicLevel)
    {
        SetMixerValue(MusicVolume, musicLevel);
    }
    
    public void OnSFXVolumeChanged(System.Single sfxLevel)
    {
        SetMixerValue(SFXVolume, sfxLevel);
    }

    private void SetMixerValue(string key, System.Single value)
    {
        value = value == 0 ? -80 : Mathf.Log10(value) * 20;
        _mixer.SetFloat(key, value);
    }
}
