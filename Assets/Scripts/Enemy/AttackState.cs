using System.Collections;
using System.Runtime.InteropServices;
using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class AttackState : FsmState
    {
        private float _attackCD;
        private Enemy _enemy;

        private float timer;

        public AttackState(Fsm fsm, Enemy enemy, float attackCD) : base(fsm)
        {
            _attackCD = attackCD;
            _enemy = enemy;
        }

        public override void Enter()
        {
            Debug.Log("Attack state enter");
            timer = 0;
        }

        public override void Update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= _attackCD && IsPlayerInRange())
            {
                timer = 0;
            }
        }

        private bool IsPlayerInRange()
        {
            RaycastHit2D hit = _enemy.CheckForPlayer();
            return hit;
        }
    }
}