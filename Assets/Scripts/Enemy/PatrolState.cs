using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class PatrolState : FsmState
    {
        private Enemy _enemy;
        private float _speed;
        private Transform[] _waypoints;
        private IMove _move;
        private int _currentIndex;
        private Vector3 _currentPosition => _enemy.transform.position;

        public PatrolState(Fsm fsm, Enemy enemy, float speed, Transform[] waypoints) : base(fsm)
        {
            _enemy = enemy;
            _speed = speed;
            _waypoints = waypoints;
            _move = enemy.GetComponent<IMove>();
        }

        public override void Update(float deltaTime)
        {
            Patrol();
            CheckForPlayer();
        }

        public override void Exit()
        {
            _move.SetDirection(Vector2.zero, 0);
        }

        private void Patrol()
        {
            Vector3 toNext = _waypoints[_currentIndex].position - _currentPosition; 
            toNext = new Vector2(toNext.x, 0).normalized;

            _enemy.SetForwardVector(toNext);
            _move.SetDirection(toNext, _speed);

            if (Mathf.Abs(_currentPosition.x - _waypoints[_currentIndex].position.x) < 0.1f)
            {
                _currentIndex = (_currentIndex + 1) % _waypoints.Length;
                Fsm.SetState<IdleState>();
            }
        }

        private void CheckForPlayer()
        {
            if (_enemy.CheckForPlayer())
                Fsm.SetState<AttackState>();
        }
    }
}