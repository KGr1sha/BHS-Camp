using UnityEngine;

namespace BHSCamp
{
    public class Ground : MonoBehaviour
    {
        public bool OnGround { get; private set; }
        public float Friction { get; private set; }

        private Vector2 _normal;
        private PhysicsMaterial2D _material;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnGround = false;
            Friction = 0;
            _animator.SetBool("IsJumping", true);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void EvaluateCollision(Collision2D collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                _normal = collision.GetContact(i).normal;
                OnGround |= _normal.y >= 0.6f;
            }
            _animator.SetBool("IsJumping", !OnGround);
        }

        private void RetrieveFriction(Collision2D collision)
        {
            _material = collision.rigidbody.sharedMaterial;

            Friction = 0;

            if(_material != null)
            {
                Friction = _material.friction;
            }
        }
    }
}
