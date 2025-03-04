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
        private const float ShotAngle = 180f;

        [SerializeField] private Bullet _bulletPrefab;

        private Transform _target;
        private int _damage;
        private float _bulletSpeed;
        private float _shotCooldown;
        private float _shotCooldownRemaining;

        public void Init(int damage, float bulletSpeed, float shotCooldown)
        {
            _damage = damage;
            _bulletSpeed = bulletSpeed;
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
            Bullet instance = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

            float randomAngle = Random.Range(0, ShotAngle);
            Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
            Vector2 targetPosition = (Vector2)transform.position + direction * 10f;

            instance.Init(_damage, _bulletSpeed, targetPosition);
        }

        private void Hide() => gameObject.SetActive(false);
    }
}
