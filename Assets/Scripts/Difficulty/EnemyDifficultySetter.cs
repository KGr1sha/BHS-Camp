using UnityEngine;

namespace BHSCamp
{
    public class EnemyDifficultySetter : MonoBehaviour 
    {
        private Health _health;
        private AttackBase _attack;
        private InstantDamageDealer _damageDealer;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _attack = GetComponent<AttackBase>();
            _damageDealer = GetComponent<InstantDamageDealer>();
        }

        private void Start()
        {
            SetDifficulty(DifficultyManager.Instance.Difficulty);
        }
        
        private void SetDifficulty(DifficultyData difficulty)
        {
            _health.SetMaxHPMultiplier(difficulty.EnemyHPMultiplier);
            _attack?.SetDamageMultiplier(difficulty.EnemyDamageMultiplier);
            _damageDealer?.SetDamageMultiplier(difficulty.EnemyDamageMultiplier);
        }
    }
}