using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonSoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        ButtonSound.OnButtonClicked += PlaySound;
    }
    
    private void OnDisable()
    {
        ButtonSound.OnButtonClicked -= PlaySound;
    }

    private void PlaySound(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);        
    }
}
