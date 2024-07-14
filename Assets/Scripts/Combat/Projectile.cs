using UnityEngine;

namespace BHSCamp
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _body;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }

        public void SetDirection(Vector2 direction)
        {
            _body.velocity = direction * _speed;
        }
    }
}