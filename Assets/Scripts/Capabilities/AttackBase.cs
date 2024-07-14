using UnityEngine;

namespace BHSCamp
{
    public abstract class AttackBase : MonoBehaviour
    {
        public bool IsAttacking { get; protected set; }
        [SerializeField] protected AnimationClip _attackAnimationClip;
        [SerializeField] protected float _attackCD;
        protected Animator _animator;
        protected Vector3 _target;

        public abstract void BeginAttack();
        public abstract void EndAttack();

        public float GetAttackAnimationDuration()
        {
            return _attackAnimationClip.length;
        }

        public float GetAttackCD()
        {
            return _attackCD;
        }

        public void SetTarget(Vector3 target)
        {
            _target = target;
        }
    }
}