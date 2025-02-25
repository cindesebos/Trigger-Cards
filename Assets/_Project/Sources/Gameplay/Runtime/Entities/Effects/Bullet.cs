using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Bullet : MonoBehaviour
    {
        private const float LifeTime = 2.5f;

        private Vector2 _direction;
        private float _moveSpeed;
        private int _damege;


        public void Init(int damege, float moveSpeed, Vector2 targetPosition)
        {
            _moveSpeed = moveSpeed;
            _damege = damege;

            _direction = (targetPosition - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Invoke(nameof(Hide), LifeTime);
        }

        public void Update() => transform.position += (Vector3)_direction * _moveSpeed * Time.deltaTime;

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
