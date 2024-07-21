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
                ResetRigidBody();
            }
        }

        private void Fall()
        {
            _body.excludeLayers = LayerMask.GetMask("Player");
            //включаем симуляцию физики для RigidBody2D
            _body.bodyType = RigidbodyType2D.Dynamic;
            //включаем непрерывную проверку коллизий чтобы объект никуда не проваливался
            _body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            //включаем интерполяцию чтобы объект падал максимально плавно
            _body.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        private void ResetRigidBody()
        {
            _body.bodyType = RigidbodyType2D.Kinematic;
            _body.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            _body.interpolation = RigidbodyInterpolation2D.None;
        }
    }
}