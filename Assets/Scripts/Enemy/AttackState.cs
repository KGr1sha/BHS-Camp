using BHSCamp.FSM;

namespace BHSCamp
{
    public class AttackState : FsmState
    {
        private Enemy _enemy;
        private AttackBase _attack;

        private float timer;
        private float _attackCD;

        public AttackState(
            Fsm fsm,
            Enemy enemy,
            AttackBase attack
        ) : base(fsm)
        {
            _attackCD = attack.GetAttackCD();
            _enemy = enemy;
            _attack = attack;
        }

        public override void Enter()
        {
            timer = 0;
            _attack.BeginAttack();
        }

        public override void Update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= _attackCD && IsPlayerInRange())
            {
                _attack.BeginAttack();
                timer = 0;
            }

            if (IsPlayerInRange() == false && _attack.IsAttacking == false)
            {
                Fsm.SetState<PatrolState>();
            }
        }

        private bool IsPlayerInRange()
        {
            return _enemy.CheckForPlayer();
        }
    }
}