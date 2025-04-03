using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public class CharacterView : MonoBehaviour
    {
        private const string IsRunningKey = "isRunning";

        private Animator _animator;
        private CharacterMovement _movement;
        private SpriteRenderer _spriteRenderer;

        public void Init(CharacterMovement movement)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _movement = movement;

            _movement.MoveComputed += OnRunning;
            _movement.MoveComputed += Flip;
        }

        private void OnDestroy()
        {
            _movement.MoveComputed -= OnRunning;
            _movement.MoveComputed -= Flip;
        }

        private void OnRunning(Vector2 direction)
        {
            float magnitudedDirection = direction.magnitude;
            if(magnitudedDirection == 0) _animator.SetBool(IsRunningKey, false);
            else _animator.SetBool(IsRunningKey, true);
        }

        private void Flip(Vector2 direction)
        {
            if(direction.x > 0 && _spriteRenderer.flipX) _spriteRenderer.flipX = false;
            else if(direction.x < 0 && !_spriteRenderer.flipX) _spriteRenderer.flipX = true;
        }
    }
}
