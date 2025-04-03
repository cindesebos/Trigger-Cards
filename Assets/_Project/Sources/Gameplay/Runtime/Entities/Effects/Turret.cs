using UnityEngine;
using Zenject;
using System.Linq;
using System.Collections.Generic;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Turret : Effect
    {
        private const float LifeTime = 60f;
        private const float BulletLifeTime = 2.5f;
        private const float ShotAngle = 180f;

        [SerializeField] private TurretBullet _bulletPrefab;

        private Transform _target;
        private int _damage;
        private float _maxBulletSpeed;
        private float _shotCooldown;
        private float _shotCooldownRemaining;

        public void Init(int damage, float maxBulletSpeed, float shotCooldown)
        {
            _damage = damage;
            _maxBulletSpeed = maxBulletSpeed;
            _shotCooldown = shotCooldown;
            _shotCooldownRemaining = _shotCooldown;

            Invoke(nameof(Hide), LifeTime);
        }

        private void Update()
        {
            if(_shotCooldownRemaining <= 0)
            {
                Shoot();
                _shotCooldownRemaining = _shotCooldown;
            }
            else _shotCooldownRemaining -= Time.deltaTime;
        }

        private void Shoot()
        {
            TurretBullet instance = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

            float randomAngle = Random.Range(0, ShotAngle);
            Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
            Vector2 targetPosition = (Vector2)transform.position + direction * 10f;

            instance.Init(_damage, _maxBulletSpeed, targetPosition, this.transform.position);
        }

        private void Hide() => gameObject.SetActive(false);
    }
}
