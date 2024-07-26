using System;
using BHSCamp;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _movementParticle;
    [SerializeField] private ParticleSystem _landingParticle;
    [Range(0, 10), SerializeField] private float _velocityTreshold;
    [Range(0, 0.2f), SerializeField] private float _dustFormationPeriod;
    [SerializeField] private Transform _player;

    private Rigidbody2D _rigidbody;
    private Ground _ground;
    private float _timer;

    private void OnEnable()
    {
        _ground.OnLand += EmitLandingParticles;
    }
    
    private void OnDisable()
    {
        _ground.OnLand -= EmitLandingParticles;
    }

    private void Awake()
    {
        _rigidbody = _player.GetComponent<Rigidbody2D>();
        _ground = _player.GetComponent<Ground>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (Mathf.Abs(_rigidbody.velocity.x) > _velocityTreshold
            && _ground.OnGround
            && _timer > _dustFormationPeriod)
        {
            _timer = 0;
            _movementParticle.Play();
        }
    }

    private void EmitLandingParticles()
    {
        _landingParticle.Play();
    }
}
