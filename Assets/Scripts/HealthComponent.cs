using UnityEngine;
using UnityEngine.Events;

namespace BHSCamp
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _currentHealth;

        #region Events
        public UnityEvent OnDeath;
        public UnityEvent OnDamageTaken;
        public UnityEvent OnHealthRestored;
        #endregion

        public float Health
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
                if (_currentHealth <= 0) Die();
            }
        }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        private void Die()
        {
            OnDeath.Invoke();
            Destroy(gameObject);
        }

        public void TakeDamage(float amount)
        {
            if (amount < 0) return;

            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            OnDamageTaken.Invoke();

            if (_currentHealth <= 0) Die();
        }

        //TODO: Homework - transfer SetMaxHealth() && RestoreHealth() to IHealable interface

        //public void SetMaxHealth(float newMaxHealth)
        //{
        //    _maxHealth = newMaxHealth;
        //    _currentHealth = _maxHealth;
        //}

        //public void RestoreHealth(float amount)
        //{
        //    if (amount < 0) return;

        //    _currentHealth += amount;
        //    _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        //    OnHealthRestored.Invoke();
        //}
    }
}