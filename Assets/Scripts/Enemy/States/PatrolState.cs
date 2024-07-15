using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class PatrolState : FsmState
    {
        private PatrolEnemy _enemy;
        private Animator _animator;
        private Rigidbody2D _body;
        private float _speed;
        private Transform[] _waypoints;
        private IMove _move;
        private int _currentIndex;
        private Vector3 _currentPosition => _enemy.transform.position;

        public PatrolState(Fsm fsm, PatrolEnemy enemy, float speed, Transform[] waypoints) : base(fsm)
        {
            _enemy = enemy;
            _speed = speed;
            _waypoints = waypoints;
            _move = enemy.GetComponent<IMove>();
            _animator = enemy.GetComponent<Animator>();
            _body = enemy.GetComponent<Rigidbody2D>();
        }

        public override void Update(float deltaTime)
        {
            Patrol();
            CheckForPlayer();
        }

        public override void Exit()
        {
            _move.SetVelocity(Vector2.zero, 0);
            _animator.SetFloat("VelocityX", 0);
        }

        //1) ищем направление до след. точки и передвигаемся к ней
        //2) если дошли до точки, переключаемя на след. и заходим в состояние Idle
        private void Patrol()
        {
            Vector3 toNext = _waypoints[_currentIndex].position - _currentPosition; 
            toNext = new Vector2(toNext.x, 0).normalized;

            _enemy.SetForwardVector(toNext);
            _move.SetVelocity(toNext, _speed);
            _animator.SetFloat("VelocityX", Mathf.Abs(_body.velocity.x));

            if (Mathf.Abs(_currentPosition.x - _waypoints[_currentIndex].position.x) < 0.1f)
            {
                _currentIndex = (_currentIndex + 1) % _waypoints.Length;
                Fsm.SetState<IdleState>();
            }
        }

        private void CheckForPlayer()
        {
            if (_enemy is EnemyWithAttack e && e.PlayerInSight())
                Fsm.SetState<AttackState>();
        }
    }
}