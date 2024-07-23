using System;
using UnityEngine;

namespace BHSCamp
{
    //компонент, отвечающий за здоровье сущности
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private int _defaultMaxHealth;
        public event Action OnDeath; //событие, вызываемое при смерти
        public event Action<int> OnDamageTaken; //событие, вызываемое при получении урона
        public event Action<int> OnHealed; //событие, вызываемое при восстановлении здоровья

        private int _maxHealth;
        public int MaxHealth { get {return _maxHealth;} }

        private int _currentHealth;
        public int CurrentHealth { get {return _currentHealth;} }

        private float _hpMultiplier = 1f;

        public void TakeDamage(int amount)
        {
            if (amount < 0) //нельзя нанести отрицательный урон
                throw new ArgumentOutOfRangeException($"Damage amount can't be negative!: {gameObject.name}");

            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _defaultMaxHealth); //здоровье не может быть < 0

            OnDamageTaken?.Invoke(amount);
            if (_currentHealth == 0) 
                OnDeath?.Invoke();
        }

        public void Heal(int amount)
        {
            if (amount < 0) // нельзя захилить отрицательное кол-во хп
                throw new ArgumentOutOfRangeException($"amount should be positive: {gameObject.name}");
            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _defaultMaxHealth);
            OnHealed?.Invoke(amount);
        }

        public void SetMaxHPMultiplier(float multiplier)
        {
            _maxHealth = (int)(_defaultMaxHealth * multiplier);
            _currentHealth = _maxHealth;
            _hpMultiplier = multiplier;
        }
    }
}