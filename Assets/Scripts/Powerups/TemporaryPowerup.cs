using System;
using UnityEngine;

namespace BHSCamp
{
    public abstract class TemporaryPowerup : PowerupBase
    {
        [SerializeField] protected float _duration;
        protected Action _onExpire;

        public override void Apply(GameObject target)
        {
            ActionOnTimer executer = GetComponent<ActionOnTimer>();
            if (executer != null)
                executer.ActionAfterTime(_onExpire, _duration);
        }
    }
}