using UnityEngine;
using System;
using Sources.Gameplay.Runtime.Infrastructure;

namespace Sources.Gameplay.Runtime.Entities
{
    public class EnemyMoveState : State
    {
        public event Action MoveStateEntered;
        public event Action<Vector2> MoveComputed;
        public event Action MoveStateExited;

        private readonly float _speed;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Transform _target;
        private readonly Transform _currentTransform;
        private readonly Enemy _enemy;

        public EnemyMoveState(StateMachine stateMachine, EnemyData data, 
        Rigidbody2D rigidbody2D, Transform target, Transform currentTransform, Enemy enemy) : base(stateMachine)
        {
            _speed = data.Speed;
            _rigidbody2D = rigidbody2D;
            _target = target;
            _currentTransform = currentTransform;
            _enemy = enemy;
        }

        public override void Enter() => MoveStateEntered?.Invoke();

        public override void Exit() => MoveStateExited?.Invoke();

        public override void Update()
        {
            if(_target.Equals(null) || _enemy.IsStunned())
            {
                StateMachine.SetState<EnemyIdleState>();
            }
        }

        public override void FixedUpdate()
        {
            if(_target.Equals(null) || _enemy.IsStunned()) return;

            Move();
        }

        private void Move()
        {
            Vector2 direction = (_target.position - _currentTransform.position).normalized;

            MoveComputed?.Invoke(direction);

            _rigidbody2D.MovePosition(_rigidbody2D.position + direction * _speed * Time.fixedDeltaTime);
        }
    }
}