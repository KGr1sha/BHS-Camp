using System;
using UnityEngine;

namespace BHSCamp
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        public event Action OnDeath;
        public event Action OnDamageTaken;

        [SerializeField] private float _maxHealth = 100f;

        private float _currentHealth;

        public float Health { get {return _currentHealth;} }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (amount < 0) return;

            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

            if (_currentHealth == 0) 
            {
                OnDeath?.Invoke();
            }
            else
            {
                OnDamageTaken?.Invoke();
            }
        }

        // HOMEWORK:--> declare SetMaxHealth() && RestoreHealth() in IHealable interface
        // and implement here

        //public void SetMaxHealth(float newMaxHealth)
        //{
        //}

        //public void RestoreHealth(float amount)
        //{
        //}
    }
}