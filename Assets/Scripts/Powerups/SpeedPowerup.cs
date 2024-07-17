using UnityEngine;

namespace BHSCamp
{
    public class SpeedPowerup : TemporaryPowerup
    {
        [SerializeField] private float _speedMultiplier;
        private IMove _move;

        public override void Apply(GameObject target)
        {
            _move = target.GetComponent<IMove>();
            _move.SetVelocityMultiplier(_speedMultiplier);
            base.Apply(target);
        }

        protected override void OnExpire()
        {
            _move.SetVelocityMultiplier(1f);
        }
    }
}