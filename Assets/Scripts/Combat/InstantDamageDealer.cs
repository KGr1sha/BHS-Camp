using UnityEngine;

namespace BHSCamp
{
    // компонент, который наносит урон при касании и откидывает
    [RequireComponent(typeof(Collision2D))]
    public class InstantDamageDealer : MonoBehaviour
    {
        [SerializeField] private int _instantDamage;
        [SerializeField] private bool _knockbackApplied;
        [SerializeField] private float _knockbackForce;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DealInstantDamage(collision.gameObject.GetComponent<IDamageable>());
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            DealInstantDamage(collider.GetComponent<IDamageable>());
        }

        private void DealInstantDamage(IDamageable damageable)
        {
            // если объект, с которым произошла коллизия, не имеет компонент IDamageable,
            //ничего не делаем
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
            //ForceMode2D.Impulse - мгновенное применение силы
            rb.AddForce(direction * knockForce, ForceMode2D.Impulse);
        }
    }
}