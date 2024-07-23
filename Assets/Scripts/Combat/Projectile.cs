using UnityEngine;

namespace BHSCamp
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private InstantDamageDealer _damageDealer;
        private Rigidbody2D _body;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _damageDealer = GetComponent<InstantDamageDealer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }

        public void SetDirection(Vector2 direction)
        {
            _body.velocity = direction * _speed;
        }

        public void SetDamageMultiplier(float multiplier)
        {
            _damageDealer.SetDamageMultiplier(multiplier);
        }
    }
}