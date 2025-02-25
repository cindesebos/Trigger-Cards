using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public class EnemyView : MonoBehaviour
    {
        private const string IsRunningKey = "isRunning";

        private Animator _animator;
        private Enemy _enemy;
        private SpriteRenderer _spriteRenderer;
        private EnemyMoveState _moveState;
        private SpriteRenderer _buffIcon;

        public void Init(EnemyMoveState moveState, Enemy enemy, SpriteRenderer buffIcon)
        {
            _enemy = enemy;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _buffIcon = buffIcon;
            _moveState = moveState;

            _moveState.MoveStateEntered += OnMoveStateEntered;
            _moveState.MoveComputed += Flip;
            _moveState.MoveStateExited += OnMoveStateExited;
            _enemy.BuffApplied += OnBuffApplied;
            _enemy.BuffRemoved += OnBuffRemoved;
        }

        private void OnDestroy()
        {
            _moveState.MoveStateEntered -= OnMoveStateEntered;
            _moveState.MoveComputed -= Flip;
            _moveState.MoveStateExited -= OnMoveStateExited;
            _enemy.BuffApplied -= OnBuffApplied;
            _enemy.BuffRemoved += OnBuffRemoved;
        }

        private void OnMoveStateEntered() => _animator.SetBool(IsRunningKey, true);
        
        private void OnMoveStateExited() => _animator.SetBool(IsRunningKey, false);

        private void Flip(Vector2 direction)
        {
            if(direction.x > 0 && _spriteRenderer.flipX) _spriteRenderer.flipX = false;
            else if(direction.x < 0 && !_spriteRenderer.flipX) _spriteRenderer.flipX = true;
        }

        private void OnBuffApplied(Sprite buffIcon) => _buffIcon.sprite = buffIcon;

        private void OnBuffRemoved() => _buffIcon.sprite = null;
    }
}
