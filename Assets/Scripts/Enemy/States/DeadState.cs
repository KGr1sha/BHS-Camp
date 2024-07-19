using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class DeadState : FsmState
    {
        private PatrolEnemy _enemy;
        private Animator _animator;
        private Collider2D _collider;
        private bool _respawn;
        private float _respawnTime;
        private float _timer;

        public DeadState(Fsm fsm, PatrolEnemy enemy, bool respawn, float respawnTime) : base(fsm)
        {
            _respawn = respawn;
            _respawnTime = respawnTime;
            _animator = enemy.Visual.GetComponent<Animator>();
            _collider = enemy.GetComponent<Collider2D>();
            _enemy = enemy;
        }

        public override void Enter()
        {
            _animator.SetBool("IsDead", true);
            _collider.enabled = false;
            _timer = 0;
        }

        public override void Update(float deltaTime)
        {
            if (!_respawn) return;
            _timer += deltaTime;
            if (_timer > _respawnTime)
                Fsm.SetState<PatrolState>();
        }

        public override void Exit()
        {
            _animator.SetBool("IsDead", false);
            Health health = _enemy.GetComponent<Health>();
            health.Heal(health.MaxHealth);
            _collider.enabled = true;
        }
    }
}