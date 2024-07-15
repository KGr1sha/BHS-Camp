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
            if (IsAttacking) return;

            base.BeginAttack(); // вызываем метод BeginAttack() у родительского класса
            _animator.SetBool("IsAttacking", true);
        }

        public override void EndAttack()
        {
            base.EndAttack();
            _animator.SetBool("IsAttacking", false);
        }
    }
}