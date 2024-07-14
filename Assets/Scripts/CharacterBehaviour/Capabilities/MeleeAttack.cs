using UnityEngine;

namespace BHSCamp
{
    [RequireComponent(typeof(Animator))]
    public class MeleeAttack : AttackBase
    {
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void BeginAttack()
        {
            IsAttacking = true;
            _animator.SetBool("IsAttacking", true);
            Invoke(nameof(EndAttack), GetAttackAnimationDuration());
        }

        public override void EndAttack()
        {
            IsAttacking = false;
            _animator.SetBool("IsAttacking", false);
        }
    }
}