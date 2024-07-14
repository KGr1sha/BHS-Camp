using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class IdleState : FsmState
    {
        private Enemy _enemy;
        private float _exitTime;
        private float _timer;


        public IdleState(Fsm fsm, Enemy enemy, float exitTime) : base(fsm)
        {
            _enemy = enemy;
            _exitTime = exitTime;
        }

        public override void Enter()
        {
            _timer = 0; 
        }

        public override void Update(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer > _exitTime)
                Fsm.SetState<PatrolState>();

            if (_enemy.CheckForPlayer())
                Fsm.SetState<AttackState>();
        }
    }
}