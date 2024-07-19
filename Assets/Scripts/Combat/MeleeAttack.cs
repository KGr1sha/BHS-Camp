using UnityEngine;

namespace BHSCamp
{
    public class MeleeAttack : AttackBase
    {
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