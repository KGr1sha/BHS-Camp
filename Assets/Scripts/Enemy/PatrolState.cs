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

        public PatrolState(Fsm fsm, Enemy enemy, float speed, Transform[] waypoints) : base(fsm)
        {
            _speed = speed;
            _waypoints = waypoints;
            _enemy = enemy;
            _move = enemy.GetComponent<IMove>();
            _transform = enemy.transform;
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
            toNext = new Vector2(toNext.x, 0).normalized;

            _move.SetDirectionX(toNext.x, _speed);

            if (Mathf.Abs(_currentPosition.x - _waypoints[_currentIndex].position.x) < 0.1f)
            {
                _currentIndex = (_currentIndex + 1) % _waypoints.Length;
                _enemy.SetForwardVector(-toNext);
            }
        }

        private void CheckForPlayer()
        {
            bool hit = _enemy.CheckForPlayer();
            if (hit)
                Fsm.SetState<AttackState>();
        }
    }
}