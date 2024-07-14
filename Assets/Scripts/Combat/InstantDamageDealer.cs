using UnityEngine;

namespace BHSCamp
{
    [RequireComponent(typeof(Collision2D))]
    public class InstantDamageDealer : MonoBehaviour
    {
        [SerializeField] private float _instantDamage;
        [SerializeField] private bool _knockbackApplied;
        [SerializeField] private float _knockbackForce;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DealInstantDamage(collision.gameObject.GetComponent<IDamageable>());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            DealInstantDamage(collision.GetComponent<IDamageable>());
        }

        private void DealInstantDamage(IDamageable damageable)
        {
            if (damageable == null) return;
            MonoBehaviour mb = (MonoBehaviour)damageable;
            if (_knockbackApplied)
            {
                Rigidbody2D rb;
                rb = mb.GetComponent<Rigidbody2D>();
                Vector2 knockbackDirection = Vector2.up;
                ApplyKnockback(rb, knockbackDirection, _knockbackForce);
            }
            damageable.TakeDamage(_instantDamage);
            print($"Dealt {_instantDamage} damage to {mb.name}");
        }

        private void ApplyKnockback(Rigidbody2D rb, Vector2 direction, float knockForce)
        {
            rb.AddForce(direction * knockForce, ForceMode2D.Impulse);
        }
    }
}