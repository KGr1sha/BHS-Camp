using UnityEngine;

namespace BHSCamp
{
    public class JumpAmountPowerup : TemporaryPowerup
    {
        [SerializeField] private int _jumpAmountIncrease;
        private IJump _jump;

        public override void Apply(GameObject target)
        {
            _jump = target.GetComponent<IJump>();
            _jump.IncreaseMaxAirJumps(_jumpAmountIncrease);
            base.Apply(target);
        }

        protected override void OnExpire()
        {
            _jump.IncreaseMaxAirJumps(-_jumpAmountIncrease);
        }
    }
}