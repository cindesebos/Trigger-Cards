using UnityEngine;
using Zenject;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterHealth))]
    public class Character : Entity, IBuffable, IFreezable
    {
        [SerializeField] private Transform _dronePoint;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private CharacterHealth _health;
        [SerializeField] private CharacterView _view;
        [SerializeField] private CharacterData _data;

        private CharacterInput _input;
        public bool _isImmortality = false;
        public bool _isLaserShootingState = false;

        [Inject]
        private void Construct(CharacterInput input)
        {
            _input = input;
        }

        private void OnEnable() => _input.Enable();

        private void OnDisable() => _input.Disable();

        private void OnValidate()
        {
            _movement ??= GetComponent<CharacterMovement>();
            _health ??= GetComponent<CharacterHealth>();
            _view ??= GetComponentInChildren<CharacterView>();
        }

        public void Start()
        {
            _movement.Init(_data, _input);
            _health.Init(_data, this);
            _view.Init(_movement);
        }

        public void AddBuff(Buff buff)
        {
            if(buff) buff.Apply(this);
        }

        public void RemoveBuff(Buff buff)
        {
            if(buff) buff.Remove(this);
        }

        public void SetImmortalityState(bool state) => _isImmortality = state;

        public void SetLaserShootingState(bool state)
        {
            if(state) _input.Disable();
            else _input.Enable();
        }

        public Transform GetDronePoint() => _dronePoint;

        public bool IsImmortality() => _isImmortality;

        public bool IsLaserShootingState() => _isLaserShootingState;

        public void SetFrozen(bool state) 
        {
            if(state) _input.Disable();
            else _input.Enable();
        }
    }
}
