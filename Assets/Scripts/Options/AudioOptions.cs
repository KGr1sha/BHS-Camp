using BHSCamp;
using UnityEngine;
using UnityEngine.UI;

public struct SoundSettings
{
    public float Master;
    public float SFX;
    public float Music;
}

public class AudioOptions : MonoBehaviour
{
    [SerializeField] private Slider _masterVolume;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _sfxVolume;
    
    private SoundSettings _currentSettings;

    private void Start()
    {
        _currentSettings = SaveLoadSystem.LoadSound();
        _masterVolume.value = _currentSettings.Master;
        _musicVolume.value = _currentSettings.Music;
        _sfxVolume.value = _currentSettings.SFX;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            SaveLoadSystem.SaveSound(_currentSettings);
    }
    
    public void SetMasterVolume(System.Single value)
    {
        _currentSettings.Master = value;
    }
    
    public void SetMusicVolume(System.Single value)
    {
        _currentSettings.Music = value;
    }
    
    public void SetSFXVolume(System.Single value)
    {
        _currentSettings.SFX = value;
    }
}
