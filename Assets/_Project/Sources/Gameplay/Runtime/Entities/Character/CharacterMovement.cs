using UnityEngine;
using System;

namespace Sources.Gameplay.Runtime.Entities
{
    public class CharacterMovement : MonoBehaviour
    {
        public event Action<Vector2> MoveComputed;

        private float _speed;
        private CharacterData _data;
        private CharacterInput _input;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveInput;

        public void Init(CharacterData data, CharacterInput input)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _data = data;
            _input = input;

            _speed = _data.Speed;
        }

        private void Update() => _moveInput = GetNormalizedMoveInput();

        private void FixedUpdate() => Move();

        private void Move() => _rigidbody2D.linearVelocity = _moveInput * _speed;

        private Vector2 ReadMoveInput() => _input.Movement.Move.ReadValue<Vector2>();

        private Vector2 GetNormalizedMoveInput()
        {
            Vector2 input = ReadMoveInput();
            float magnitudedInput = input.magnitude;
            MoveComputed?.Invoke(input);
            return magnitudedInput > 0 ? input.normalized : Vector2.zero;
        }
    }
}