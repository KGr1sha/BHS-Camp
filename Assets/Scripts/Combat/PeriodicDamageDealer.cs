using UnityEngine;

namespace BHSCamp
{
    [RequireComponent(typeof(Collider2D))]
    public class PeriodicDamageDealer : MonoBehaviour
    {
        [Header("MODE: Periodic damage")]
        [SerializeField] private int _periodicDamage;
        [SerializeField] private float _periodicDamageCooldown;
        private float _periodicDamageTimer;

        private void OnTriggerStay2D(Collider2D collision)
        {
            DealPeriodicDamage(collision.gameObject.GetComponent<IDamageable>());
            _periodicDamageTimer += Time.fixedDeltaTime;
        }

        private void DealPeriodicDamage(IDamageable damageable)
        {
            if (damageable == null) return;

            if (_periodicDamageTimer >= _periodicDamageCooldown)
            {
                _periodicDamageTimer = 0;
                damageable.TakeDamage(_periodicDamage);
                MonoBehaviour mb = (MonoBehaviour) damageable;
                print($"Dealt {_periodicDamage} to {mb.name}");
            }
        }
    }
}