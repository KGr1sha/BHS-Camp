using System.Collections;
using UnityEngine;

namespace BHSCamp
{
    // враг, который патрулирует и атакует игрока, если тот в зоне видимости
    public class EnemyWithAttack : PatrolEnemy
    {
        [Header("Attack")]
        [SerializeField] protected LayerMask _playerLayerMask;
        [SerializeField] protected Vector2 _attackRange;
        protected AttackBase _attack;
        public bool CanAttack { get; private set; }

        private void Awake()
        {
            _attack = GetComponent<AttackBase>();
            _body = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
        }

        private void Start()
        {
            InitializeStates();
            _fsm.SetState<PatrolState>();
            SetForwardVector(new Vector2(transform.localScale.x, 0));
            CanAttack = true;
        }

        protected override void InitializeStates()
        {
            // кроме состояний родительского класса(PatrolEnemy) добавляем состояние атаки
            base.InitializeStates();
            _fsm.AddState(new AttackState(_fsm, this, _attack));
        }

        private void Update()
        {
            _fsm.Update(Time.deltaTime);
        }

        public virtual bool PlayerInSight()
        {
            RaycastHit2D hit = CheckPlayerHit();
            if (!hit) return false;
            return hit.collider.GetComponent<IDamageable>() != null;
        }

        public virtual RaycastHit2D CheckPlayerHit()
        {
            Vector2 origin = new(
                transform.position.x + (_forwardVector.x * _attackRange.x / 2),
                transform.position.y
            );
            RaycastHit2D hit = Physics2D.BoxCast(
                origin,
                _attackRange,
                0f,
                _forwardVector,
                0,
                _playerLayerMask
            );
            return hit;
        }

        public IEnumerator HandleAttackCD()
        {
            CanAttack = false;
            yield return new WaitForSeconds(_attack.GetAttackCD());
            CanAttack = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (_forwardVector == null) return;
            Vector2 origin = new(
                transform.position.x + (_forwardVector.x * _attackRange.x / 2),
                transform.position.y
            );
            Gizmos.DrawWireCube(origin, _attackRange);
        }
    }
}