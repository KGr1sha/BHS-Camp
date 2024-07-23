using UnityEngine;

namespace BHSCamp
{
    public class PlayerDifficultySetter : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void Start()
        {
            _health.SetMaxHPMultiplier(DifficultyManager.Instance.Difficulty.PlayerHPMultiplier);
        }
    }
}