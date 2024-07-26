using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _trackList;
    private int _currentTrackIndex;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Initialize();
        PlayNextTrack();
    }

    private void PlayNextTrack()
    {
        _audioSource.clip = _trackList[_currentTrackIndex];
        _audioSource.Play();
        StartCoroutine(PlayNextTrackAfterDelay(_audioSource.clip.length));
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

    private void Initialize()
    {
        ShuffleTracks(); 
        _currentTrackIndex = 0;
        _audioSource.pitch = 1;
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
}
