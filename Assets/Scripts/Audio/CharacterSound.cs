using System;
using BHSCamp;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterSound : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _runSound;

    [SerializeField] private AudioSource _stepAudioSource;
    private AudioSource _audioSource;
    private Ground _ground;

    private float _inputX;
    private bool _isRunning;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _ground = GetComponent<Ground>();
    }

    private void Update()
    {
        if (_isRunning == false && _ground.OnGround && _inputX != 0)
        {
            _isRunning = true;
            _stepAudioSource.clip = _runSound;
            _stepAudioSource.Play();
        }

        if (_isRunning && (_ground.OnGround == false || _inputX == 0))
        {
            _isRunning = false;
            _stepAudioSource.Stop();
        }
    }

    public void PlayJumpSound()
    {
        if (_jumpSound) 
            _audioSource.PlayOneShot(_jumpSound);
    }

    public void PlayAttackSound()
    {
        if (_attackSound) 
            _audioSource.PlayOneShot(_attackSound);
    }

    public void PlayHurtSound()
    {
        if (_hurtSound) 
            _audioSource.PlayOneShot(_hurtSound);
    }

    public void SetInputX(float inputX)
    {
        _inputX = inputX;
    }
}
