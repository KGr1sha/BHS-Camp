using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class AttackState : FsmState
    {
        private float _attackCD;
        private Enemy _enemy;
        private Animator _animator;

        private float timer;
        private bool _canExit;

        public AttackState(Fsm fsm, Enemy enemy, float attackCD, Animator animator) : base(fsm)
        {
            _attackCD = attackCD;
            _enemy = enemy;
            _animator = animator;
        }

        public override void Enter()
        {
            timer = 0;
            Attack();
        }

        public override void Update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= _attackCD && IsPlayerInRange())
            {
                Attack();
                timer = 0;
            }

            if (_canExit == false && timer >= 1f)
                _canExit = true;

            if (IsPlayerInRange() == false && _canExit)
            {
                Fsm.SetState<PatrolState>();
            }
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
            _canExit = false;
        }

        private bool IsPlayerInRange()
        {
            RaycastHit2D hit = _enemy.CheckForPlayer();
            return hit;
        }
    }
}