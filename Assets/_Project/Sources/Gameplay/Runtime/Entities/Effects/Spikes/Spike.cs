using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Spike : MonoBehaviour
    {
        private const float LifeTime = 2.5f;

        [field: SerializeField] public Vector2 MoveDirection { get; private set;}

        private float _moveSpeed;
        private int _damege;

        public void Init(float moveSpeed, int damege)
        {
            _moveSpeed = moveSpeed;
            _damege = damege;

            Invoke(nameof(Hide), LifeTime);
        }

        public void Move() => transform.position += (Vector3)MoveDirection * _moveSpeed * Time.deltaTime;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.ApplyDamage(_damege);
                gameObject.SetActive(false);
            }
        }

        private void Hide() => gameObject.SetActive(false);
    }
}
