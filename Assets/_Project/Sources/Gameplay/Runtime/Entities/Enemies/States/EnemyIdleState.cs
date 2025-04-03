using UnityEngine;
using Sources.Gameplay.Runtime.Infrastructure;

namespace Sources.Gameplay.Runtime.Entities
{
    public class EnemyIdleState : State
    {
        private readonly Transform _target;
        private readonly Enemy _enemy;

        public EnemyIdleState(StateMachine stateMachine, Transform target, Enemy enemy) : base(stateMachine)
        {
            _target = target;
            _enemy = enemy;
        }

        public override void Update()
        {
            if(!_target.Equals(null) && !_enemy.IsStunned() && !_enemy.IsFrozen())
            {
                StateMachine.SetState<EnemyMoveState>();
            }
        }
    }
}