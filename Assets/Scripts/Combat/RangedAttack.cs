using UnityEngine;

namespace BHSCamp
{
    public class RangedAttack : AttackBase
    {
        [SerializeField] private Projectile _projectilePrefab; // префаб проджектайла
        [SerializeField] private float _projectileSpawnOffset; // отступ, на котором будет спавниться проджектайл
        [SerializeField] private Transform _projectileParent;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void BeginAttack()
        {
            if (IsAttacking) return;
            base.BeginAttack();
            _animator.SetBool("IsAttacking", true);
        }

        public override void EndAttack()
        {
            base.EndAttack();
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
