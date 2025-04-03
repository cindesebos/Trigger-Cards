using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public class TurretBullet : Bullet
    {
        private const float TurretShootRange = 2.5f;

        private float _currentMoveSpeed;
        private float _timeElapsed = 0f;
        private Vector2 _turretPosition;

        public virtual void Init(int damege, float moveSpeed, Vector2 targetPosition, Vector2 turretPosition)
        {
            MoveSpeed = moveSpeed;

            Damege = damege;
            _turretPosition = turretPosition;

            Direction = (targetPosition - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Invoke(nameof(Hide), LifeTime);
        }

        public virtual void Update()
        {
            float distance = Vector2.Distance(transform.position, _turretPosition);

            if(distance < TurretShootRange)
            {
                _timeElapsed += Time.deltaTime;

                _currentMoveSpeed = Mathf.Lerp(0f, MoveSpeed, _timeElapsed / LifeTime);

                transform.position += (Vector3)Direction * _currentMoveSpeed * Time.deltaTime;
            }
            else Hide();
        }
    }
}
