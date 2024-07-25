using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace BHSCamp
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public const string MasterVolume = "Master";
        public const string MusicVolume = "Music";
        public const string SFXVolume = "SFX";

        [SerializeField] private AudioMixer _audioMixer;

        [SerializeField] private List<AudioClip> _trackList;
        private int _currentTrackIndex;

        private AudioSource _audioPlayer;

        private void Awake()
        {
            _audioPlayer = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Initialize();
            PlayNextTrack();
        }

        private void Initialize()
        {
            ShuffleTracks();
            _currentTrackIndex = 0;
            _audioPlayer.pitch = 1;
        }

        /// <summary> Проигрывание следующего трека без задержки.</summary>
        private void PlayNextTrack()
        {
            _audioPlayer.clip = _trackList[_currentTrackIndex];
            _audioPlayer.Play();
            StartCoroutine(PlayNextTrackAfterDelay(_audioPlayer.clip.length));
        }

        private IEnumerator PlayNextTrackAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (_currentTrackIndex >= _trackList.Count - 1)
            {
                ShuffleTracks();
                _currentTrackIndex = 0;
            }
            else
            {
                _currentTrackIndex++;
            }
            PlayNextTrack();
        }

        private void ShuffleTracks()
        {
            System.Random random = new();
            for (int n = _trackList.Count - 1; n > 1; n--)
            {
                int k = random.Next(n + 1);
                (_trackList[n], _trackList[k]) = (_trackList[k], _trackList[n]);
            }
        }

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
            _audioMixer.SetFloat(key, value);
        }
    }
}