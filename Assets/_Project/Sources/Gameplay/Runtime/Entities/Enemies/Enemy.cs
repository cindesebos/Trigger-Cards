using UnityEngine;
using Zenject;
using System;
using Sources.Gameplay.Runtime.Infrastructure;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    [RequireComponent(typeof(EnemyHealth))]
    public class Enemy : Entity, IBuffable, IFreezable
    {
        public event Action<Sprite> BuffApplied;
        public event Action BuffRemoved;
        public bool _isFrozen;

        [SerializeField] private EnemyData _data;
        [SerializeField] private EnemyView _view;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private SpriteRenderer _buffIcon;

        private IEntitiesObserver _entitiesObserver;
        private Rigidbody2D _rigidbody2D;
        private StateMachine _stateMachine;
        private EnemyIdleState _idleState;
        private EnemyMoveState _moveState;
        private bool _isStunned;
        public bool _isMultipleDamage;

        [Inject]
        private void Construct(IEntitiesObserver entitiesObserver, DiContainer diContainer)
        {
            _entitiesObserver = entitiesObserver;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            InitStatesMachine(_entitiesObserver.GetCharacter().Transform);

            _view.Init(_moveState, this, _buffIcon);
            _health.Init(_data, this);
        }

        private void OnValidate()
        {
            _view ??= GetComponentInChildren<EnemyView>();
            _health ??= GetComponent<EnemyHealth>();
        }

        private void InitStatesMachine(Transform target)
        {
            _stateMachine = new StateMachine();

            _idleState = new EnemyIdleState(_stateMachine, target, this);
            _moveState = new EnemyMoveState(_stateMachine, _data, _rigidbody2D, target, transform, this);

            _stateMachine.AddState(_idleState);
            _stateMachine.AddState(_moveState);

            _stateMachine.SetState<EnemyIdleState>();
        }

        private void Update() => _stateMachine.Update();

        private void FixedUpdate() => _stateMachine.FixedUpdate();

        public void AddBuff(Buff buff)
        {
            if(buff) buff.Apply(this);
        }

        public void RemoveBuff(Buff buff)
        {
            if(buff) buff.Remove(this);
        }

        public void SetStunState(bool state, Sprite debuffIcon)
        {
            if(state) BuffApplied?.Invoke(debuffIcon);
            else BuffRemoved?.Invoke();
            
            _isStunned = state;
        }

        public void SetDamageMultiplierState(bool state, Sprite debuffIcon)
        {
            if(state) BuffApplied?.Invoke(debuffIcon);
            else BuffRemoved?.Invoke();

            _isMultipleDamage = state;
        }

        public void SetToxinState(bool state, Sprite debuffIcon, int damage = 0)
        {
            if(state)
            {
                BuffApplied?.Invoke(debuffIcon);
                _health.ApplyDamage(damage);
            }
            else if(!state) BuffRemoved?.Invoke();
        }

        public bool IsStunned() => _isStunned;

        public bool IsDamageMultiplier() => _isMultipleDamage;

        public void SetFrozen(bool state) => _isFrozen = state;

        public bool IsFrozen() => _isFrozen;
    }
}
