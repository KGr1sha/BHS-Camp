using UnityEngine;

namespace BHSCamp
{
    public abstract class TemporaryPowerup : PowerupBase
    {
        [SerializeField] protected float _duration;

        public override void Apply(GameObject target)
        {
            ActionOnTimer executer = target.GetComponent<ActionOnTimer>();
            if (executer != null)
                executer.ActionAfterTime(OnExpire, _duration);
        }

        protected abstract void OnExpire();
    }
}