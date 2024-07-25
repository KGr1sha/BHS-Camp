using UnityEngine;
using UnityEngine.UI;

namespace BHSCamp.Options
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private Slider _masterVolume;
        [SerializeField] private Slider _sfxVolume;
        [SerializeField] private Slider _musicVolume;
        
        private SoundSettings _currentSettings;

        private void Start()
        {
            _currentSettings = SaveLoadSystem.LoadSound();
            _masterVolume.value = _currentSettings.Master;
            _sfxVolume.value = _currentSettings.SFX;
            _musicVolume.value = _currentSettings.Music;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
                SaveSoundSettings();
        }

        public void SetMasterVolume()
        {
            _currentSettings.Master = _masterVolume.value;
        }
        
        public void SetSFXVolume()
        {
            _currentSettings.SFX = _sfxVolume.value;
        }
        
        public void SetMusicVolume()
        {
            _currentSettings.Music = _musicVolume.value;
        }

        private void SaveSoundSettings()
        {
            SaveLoadSystem.SaveSound(_currentSettings);
        }
    }
}