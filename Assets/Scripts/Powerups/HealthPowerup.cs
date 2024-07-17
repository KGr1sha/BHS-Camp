using UnityEngine;

namespace BHSCamp
{
    public class HealthPowerup : PowerupBase 
    {
        [SerializeField] private int _healAmount;

        public override void Apply(GameObject target)
        {
            target.GetComponent<IHealable>()?.Heal(_healAmount);
        }
    }
}