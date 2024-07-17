using System;
using UnityEngine;

namespace BHSCamp
{
    public class PowerupApplier : MonoBehaviour
    {
        public static event Action OnPowerupCollected;
        private PowerupBase[] _powerups;

        private void Awake()
        {
            _powerups = GetComponents<PowerupBase>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            ApplyPowerups(collider.gameObject);
            OnPowerupCollected?.Invoke();
            Destroy(gameObject);
        }

        private void ApplyPowerups(GameObject target)
        {
            foreach(PowerupBase powerup in _powerups)
                powerup.Apply(target);
        }
    }
}