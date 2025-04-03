using UnityEngine;
using Zenject;
using System.Linq;
using System.Collections.Generic;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Drone : Effect
    {
        private const float LifeTime = 60f;

        [SerializeField] private Bullet _bulletPrefab;

        private Transform _target;
        private int _damage;
        private float _moveSpeed;
        private float _bulletSpeed;
        private float _shotCooldown;
        private IEntitiesObserver _entitiesObserver;
        private float _shotCooldownRemaining;

        public void Init(int damage, float moveSpeed, float bulletSpeed, float shotCooldown, IEntitiesObserver entitiesObserver)
        {
            _damage = damage;
            _moveSpeed = moveSpeed;
            _bulletSpeed = bulletSpeed;
            _shotCooldown = shotCooldown;
            _entitiesObserver = entitiesObserver;
            _target = _entitiesObserver.GetCharacter().GetDronePoint();
            _shotCooldownRemaining = _shotCooldown;

            Invoke(nameof(Hide), LifeTime);
        }

        private void Update()
        {
            Move();

            if(_shotCooldownRemaining <= 0)
            {
                Enemy enemy = FindClosestEnemy();
                if(enemy != null )
                {
                    Shoot(enemy);
                    _shotCooldownRemaining = _shotCooldown;
                }
            }
            else _shotCooldownRemaining -= Time.deltaTime;
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);
        }

        private void Shoot(Enemy enemy)
        {
            Bullet instance = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

            instance.Init(_damage, _bulletSpeed, enemy.Transform.position);
        }

        private Enemy FindClosestEnemy()
        {
            if(_entitiesObserver == null) return null;
            
            List<Enemy> enemies = _entitiesObserver.GetAllEnemies().ToList();

            float minimalDistance = Mathf.Infinity;
            Enemy closestEnemy = null;

            foreach(Enemy enemy in enemies)
            {
                if(enemy.gameObject.activeInHierarchy == false) return null;

                float distance = Vector2.SqrMagnitude(enemy.Transform.position - transform.position);

                if(distance < minimalDistance)
                {
                    minimalDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

        private void Hide() => gameObject.SetActive(false);
    }
}
