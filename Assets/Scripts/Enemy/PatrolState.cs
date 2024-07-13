using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class PatrolState : FsmState
    {
        private float _speed;
        private Transform[] _waypoints;
        private IMove _move;
        private int _currentIndex;
        private Transform _transform;
        private Enemy _enemy;
        private Vector3 _currentPosition => _transform.position;

        public PatrolState(Fsm fsm, Enemy enemy, float speed, Transform[] waypoints, IMove move, Transform transform) : base(fsm)
        {
            _speed = speed;
            _waypoints = waypoints;
            _move = move;
            _transform = transform;
            _enemy = enemy;
        }

        public override void Update(float deltaTime)
        {
            Patrol();
            CheckForPlayer();
        }

        public override void Exit()
        {
            _move.SetDirectionX(0, 0);
        }

        private void Patrol()
        {
            Vector3 toNext = _waypoints[_currentIndex].position - _currentPosition; 
            toNext.Normalize();

            _move.SetDirectionX(toNext.x, _speed);
            _enemy.SetForwardVector(new Vector2(toNext.x, 0));

            if (Vector3.Distance(_currentPosition, _waypoints[_currentIndex].position) < 0.1f)
            {
                _currentIndex = (_currentIndex + 1) % _waypoints.Length;
            }
        }

        private void CheckForPlayer()
        {
            RaycastHit2D hit = _enemy.CheckForPlayer();

            if (hit)
            {
                Fsm.SetState<AttackState>();
            }
        }
    }
}