using System;
using BHSCamp;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _movementParticle;
    [SerializeField] private ParticleSystem _landParticle;

    [Range(0, 10)]
    [SerializeField] private float _velocityThreshold;
    
    [Range(0, 0.2f)]
    [SerializeField] private float _dustFormationPeriod;

    private Rigidbody2D _rigidbody;
    private Ground _ground;

    private float _counter;

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
        _rigidbody = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
    }

    private void Update()
    {
        _counter += Time.deltaTime;

        if (Mathf.Abs(_rigidbody.velocity.x) > _velocityThreshold && _ground.OnGround)
        {
            if (_counter > _dustFormationPeriod)
            {
                _movementParticle.Play();
                _counter = 0;
            }
        }
    }

    private void EmitLandingParticles()
    {
        _landParticle.Play();
    }
}
