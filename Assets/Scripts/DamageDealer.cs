using UnityEngine;

namespace BHSCamp
{
    //[RequireComponent(typeof(Collider2D))]
    public class DamageDealer : MonoBehaviour
    {
        private enum DamageMode
        {
            Instant,
            Periodic
        }

        [Header("Choose MODE")]
        [SerializeField] private DamageMode mode;

        [Header("MODE: Instant damage")]
        [SerializeField] private bool knockbackApplied;
        [SerializeField] private float instantDamage;
        [SerializeField] private float knockbackForce;


        [Header("MODE: Periodic damage")]
        [SerializeField] private float periodDamage;
        [SerializeField] private float timePeriod;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (mode)
            {
                case DamageMode.Instant:
                    DealInstantDamage(collision.gameObject.GetComponent<IDamageable>());
                    break;
                default:
                    return;
            }
        }

        private void DealInstantDamage(IDamageable damageable)
        {
            if (knockbackApplied)
            {
                Rigidbody2D rb;
                if (damageable is MonoBehaviour mb)
                {
                    rb = mb.GetComponent<Rigidbody2D>();
                    Vector2 knockbackDirection = (mb.transform.position - transform.position).normalized;
                    print($"Knock dir: {knockbackDirection}");
                    ApplyKnockback(rb, knockbackDirection, knockbackForce);
                }
            }
            damageable.TakeDamage(instantDamage);
        }

        private void ApplyKnockback(Rigidbody2D rb, Vector2 direction, float knockForce)
        {
            rb.AddForce(direction * knockForce, ForceMode2D.Impulse);
        }
    }
}