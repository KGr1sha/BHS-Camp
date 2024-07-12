using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class Enemy : MonoBehaviour
    {
        [Header("Patroling")]
        [SerializeField] private float _patrolSpeed = 5f;
        [SerializeField] private Transform[] _waypoints;

        [Header("Box Cast")]
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private Vector2 _boxCastSize;
        [Header("Attack")]
        [SerializeField] private float _attackCD = 1f;

        private Animator _animator;
        private Rigidbody2D _body;

        private Fsm _fsm;
        private IMove _move;

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

        public RaycastHit2D CheckForPlayer()
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                transform.position,
                _boxCastSize,
                0f,
                transform.right,
                _boxCastSize.x / 2,
                _playerLayerMask
            );
            return hit;
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

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector2 origin = new Vector2(
                transform.right.x * (transform.position.x + _boxCastSize.x / 2),
                transform.position.y
            );
            Gizmos.DrawWireCube(origin, _boxCastSize);
        }
    }
}