using UnityEngine;

namespace BHSCamp
{
    //базовый класс для компонента атаки
    public abstract class AttackBase : MonoBehaviour
    {
        public bool IsAttacking { get; protected set; }
        [SerializeField] protected AnimationClip _attackAnimationClip; //анимация атаки
        [SerializeField] protected float _attackCD; //кулдаун атаки
        [SerializeField] protected Animator _animator;
        protected Vector3 _target;

        public virtual void BeginAttack()
        {
            IsAttacking = true;
            Invoke(nameof(EndAttack), GetAttackAnimationDuration());
        }

        public virtual void EndAttack()
        {
            IsAttacking = false;
        }

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