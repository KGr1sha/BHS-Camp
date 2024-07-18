using UnityEngine;

namespace BHSCamp
{
    public class HealthPowerup : MonoBehaviour, IPowerup
    {
        [SerializeField] private int _healAmount;

        public void Apply(GameObject target)
        {
            target.GetComponent<IHealable>()?.Heal(_healAmount);
        }
    }
}