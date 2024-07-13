using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class Enemy : MonoBehaviour
    {
        [Header("Patroling")]
        [SerializeField] private float _patrolSpeed = 5f;
        [SerializeField] private Transform[] _waypoints;

        [Header("Attack")]
        [SerializeField] private float _attackCD = 1f;
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private Vector2 _attackRange;

        private Animator _animator;
        private Rigidbody2D _body;
        private IMove _move;

        private Fsm _fsm;
        private Vector2 _forwardVector;

        private void Start()
        {
            _move = GetComponent<IMove>();
            _animator = GetComponent<Animator>();
            _body = GetComponent<Rigidbody2D>();
            _fsm = new Fsm();
            _fsm.AddState(new PatrolState(_fsm, this, _patrolSpeed, _waypoints, _move, transform));
            _fsm.AddState(new AttackState(_fsm, this, _attackCD, _animator));
            _fsm.SetState<PatrolState>();
        }

        private void Update()
        {
            _fsm.Update(Time.deltaTime);
            if (_body.velocity.x != 0)
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x) * Mathf.Sign(_body.velocity.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
        }

        private void FixedUpdate()
        {
            _fsm.FixedUpdate();
        }

        public void SetForwardVector(Vector2 forward)
        {
            _forwardVector = forward;
        }

        public RaycastHit2D CheckForPlayer()
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

        public void OnDrawGizmos()
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