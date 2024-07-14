using UnityEngine;

namespace BHSCamp
{
    public class RangedAttack : AttackBase
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _projectileSpawnOffset;
        [SerializeField] private Transform _projectileParent;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void BeginAttack()
        {
            if (IsAttacking) return;

            IsAttacking = true;
            _animator.SetBool("IsAttacking", true);
            Invoke(nameof(EndAttack), GetAttackAnimationDuration());
        }

        public override void EndAttack()
        {
            IsAttacking = false;
            _animator.SetBool("IsAttacking", false);

            Vector3 toTarget = (_target - transform.position).normalized;
            Projectile projectile = Instantiate(
                _projectilePrefab,
                transform.position + toTarget*_projectileSpawnOffset,
                Quaternion.identity,
                _projectileParent
            );
            projectile.SetDirection(toTarget);
        }
    }
}
