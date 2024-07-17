using UnityEngine;

namespace BHSCamp
{
    public class JumpHeightPowerup : TemporaryPowerup
    {
        [SerializeField] private float _jumpHeightMultiplier;
        private IJump _jump;

        public override void Apply(GameObject target)
        {
            _jump = target.GetComponent<IJump>();
            _jump.SetJumpHeightMultiplier(_jumpHeightMultiplier);
            base.Apply(target);
        }

        protected override void OnExpire()
        {
            _jump.SetJumpHeightMultiplier(1f);
        }
    }
}