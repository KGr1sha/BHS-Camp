using UnityEngine;

namespace BHSCamp
{
    public class JumpHeightPowerup : TemporaryPowerup
    {
        [SerializeField] private float _jumpHeightMultiplier;
        private IJump _jump;

        public override void Apply(GameObject target)
        {
            base.Apply(target);
            _jump = target.GetComponent<IJump>();
            _jump.SetJumpHeightMultiplier(_jumpHeightMultiplier);
        }
    }
}