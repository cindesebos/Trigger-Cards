using UnityEngine;
using System;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        public event Action<int> HealthChanged;

        public int Health
        {
            get => _health;
            private set
            {
                Debug.Log("health was changed with value "+ value);
                _health = Mathf.Clamp(value, 0, _maxHealth);

                HealthChanged?.Invoke(_health);

                if(_health <= 0) Die();
            }
        }

        private int _health;
        private int _maxHealth;
        private EnemyData _data;
        private Enemy _enemy;

        public void Init(EnemyData data, Enemy enemy)
        {
            _data = data;
            _enemy = enemy;

            _maxHealth = _data.Health;
            Health = _maxHealth;
        }

        public void ApplyDamage(int damage)
        {
            if(_enemy.IsDamageMultiplier()) damage *= 2;
            
            Health -= damage;
        }

        private void Die() => gameObject.SetActive(false);
    }
}
