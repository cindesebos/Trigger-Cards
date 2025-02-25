using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterHealth))]
    public class Character : Entity
    {
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private CharacterHealth _health;
        [SerializeField] private CharacterView _view;
        [SerializeField] private CharacterData _data;

        private CharacterInput _input;

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
            _health.Init(_data);
            _view.Init(_movement);
        }
    }
}
