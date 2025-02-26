using UnityEngine;
using Zenject;
using System;
using System.Linq;
using System.Collections.Generic;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Plasma : Effect
    {
        public event Action<Enemy> Shot;

        private const float LifeTime = 8f;

        [SerializeField] private PlasmaView _view;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private int _damage;
        private float _moveSpeed;
        private float _shotCooldown;
        private float _shotRange;
        private LayerMask _enemyLayer;
        private IEntitiesObserver _entitiesObserver;
        private float _shotCooldownRemaining;

        private void OnValidate()
        {
            _view ??= GetComponent<PlasmaView>();
            _rigidbody2D ??= GetComponent<Rigidbody2D>();
        }

        public void Init(int damage, float moveSpeed, float shotCooldown, float shotRange, IEntitiesObserver entitiesObserver)
        {
            _damage = damage;
            _moveSpeed = moveSpeed;
            _shotCooldown = shotCooldown;
            _shotCooldownRemaining = _shotCooldown;
            _shotRange = shotRange;
            _entitiesObserver = entitiesObserver;
            _view.Init(this);
            _rigidbody2D.linearVelocity = transform.right * _moveSpeed;

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
            List<Enemy> enemies = _entitiesObserver.GetAllEnemies().ToList();

            foreach(var enemy in enemies)
            {
                if(Vector2.SqrMagnitude(enemy.Transform.position - transform.position) > _shotRange || !enemy.gameObject.activeInHierarchy) return;

                Shot?.Invoke(enemy);
                enemy.GetComponent<EnemyHealth>().ApplyDamage(_damage);
            }
        }

        private void Hide() => gameObject.SetActive(false);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, _shotRange);
        }
    }
}
