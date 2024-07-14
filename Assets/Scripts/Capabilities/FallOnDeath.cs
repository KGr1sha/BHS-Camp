using UnityEngine;

namespace BHSCamp
{
    public class FallOnDeath : MonoBehaviour
    {
        private Ground _ground;
        private Animator _animator;
        private Health _health;
        private Rigidbody2D _body;

        private void OnEnable()
        {
            _health.OnDeath += Fall;
        }

        private void OnDisable()
        {
            _health.OnDeath -= Fall;
        }

        private void Awake()
        {
            _ground = GetComponent<Ground>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            _body = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_ground.OnGround)
            {
                _animator.SetBool("IsOnGround", true);
            }
        }

        private void Fall()
        {
            _body.bodyType = RigidbodyType2D.Dynamic;
            _body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            _body.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}