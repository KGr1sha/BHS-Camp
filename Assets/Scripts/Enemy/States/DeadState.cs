using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class DeadState : FsmState
    {
        private PatrolEnemy _enemy;
        private Animator _animator;
        private bool _respawn;
        private float _respawnTime;

        public DeadState(Fsm fsm, PatrolEnemy enemy, bool respawn, float respawnTime) : base(fsm)
        {
            _respawn = respawn;
            _respawnTime = respawnTime;
            _animator = enemy.GetComponent<Animator>();
            _enemy = enemy;
        }

        public override void Enter()
        {
            _animator.SetBool("IsDead", true);
        }

        // STEP 11: Если _respawn == true,
        // через _respawnTime секунд мы должны выйти из состояния Dead
        // и восстановить здоровье
    }
}