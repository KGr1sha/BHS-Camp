using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class HurtState : FsmState
    {
        private Animator _animator;
        private float _exitTime;
        private float _timer;
        private Health _health;

        public HurtState(Fsm fsm, PatrolEnemy enemy, float exitTime) : base(fsm)
        {
            _exitTime = exitTime;
            _animator = enemy.GetComponent<Animator>();
            _health = enemy.GetComponent<Health>();
        }

        public override void Enter()
        {
            _animator.SetTrigger("Hurt");
            _timer = 0;
            
            if (0 == _health.CurrentHealth)
                Fsm.SetState<DeadState>();
        }

        public override void Update(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer >= _exitTime)
                Fsm.SetState<PatrolState>();
        }
    }
}