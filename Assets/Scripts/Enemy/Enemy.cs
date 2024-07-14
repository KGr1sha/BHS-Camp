using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class Enemy : MonoBehaviour
    {
        [Header("Patroling")]
        [SerializeField] private float _patrolSpeed;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _idleTime;
        [Header("Attack")]
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private Vector2 _attackRange;
        [Header("Other")]
        [SerializeField] private float _staggerTime;

        private Animator _animator;
        private AttackBase _attack;
        private Rigidbody2D _body;
        private HealthComponent _health;

        private Fsm _fsm;
        private Vector2 _forwardVector;

        private void OnEnable()
        {
            _health.OnDamageTaken += OnHit;
        }

        private void OnDisable()
        {
            _health.OnDamageTaken -= OnHit;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _attack = GetComponent<AttackBase>();
            _body = GetComponent<Rigidbody2D>();
            _health = GetComponent<HealthComponent>();
        }

        private void Start()
        {
            _fsm = new Fsm();
            _fsm.AddState(new PatrolState(_fsm, this, _patrolSpeed, _waypoints));
            _fsm.AddState(new AttackState(_fsm, this, _attack));
            _fsm.AddState(new HurtState(_fsm, _staggerTime, _animator));
            _fsm.AddState(new IdleState(_fsm, this, _idleTime));
            _fsm.SetState<PatrolState>();
            SetForwardVector(new Vector2(transform.localScale.x, 0));
        }

        private void Update()
        {
            _animator.SetFloat("VelocityX", Mathf.Abs(_body.velocity.x));
            _fsm.Update(Time.deltaTime);
        }

        public void SetForwardVector(Vector2 forward)
        {
            _forwardVector = forward;
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * _forwardVector.x,
                transform.localScale.y,
                transform.localScale.z
            );
        }

        public bool CheckForPlayer()
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
            return hit.collider?.GetComponent<IDamageable>() != null;
        }

        private void OnHit()
        {
            _fsm.SetState<HurtState>();
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