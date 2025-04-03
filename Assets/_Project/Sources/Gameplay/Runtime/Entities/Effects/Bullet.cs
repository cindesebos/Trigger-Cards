using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Bullet : MonoBehaviour
    {
        protected const float LifeTime = 2.5f;

        protected Vector2 Direction;
        protected float MoveSpeed;
        protected int Damege;

        public void Init(int damege, float moveSpeed, Vector2 targetPosition)
        {
            MoveSpeed = moveSpeed;

            Damege = damege;

            Direction = (targetPosition - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Invoke(nameof(Hide), LifeTime);
        }

        public void Update() => transform.position += (Vector3)Direction * MoveSpeed * Time.deltaTime;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.ApplyDamage(Damege);
                gameObject.SetActive(false);
            }
        }

        protected void Hide() => gameObject.SetActive(false);
    }
}
