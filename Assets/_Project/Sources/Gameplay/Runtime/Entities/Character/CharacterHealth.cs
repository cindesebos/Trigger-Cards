using UnityEngine;
using System;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    public class CharacterHealth : MonoBehaviour, IDamageable, IHealable
    {
        public event Action<int> HealthChanged;

        public int Health
        {
            get => _health;
            private set
            {
                if(value < 0 || value > _data.Health) throw new ArgumentOutOfRangeException();

                _health = value;

                HealthChanged?.Invoke(_health);

                if(_health <= 0) Die();
            }
        }

        private int _health;
        private CharacterData _data;

        public void Init(CharacterData data)
        {
            _data = data;

            Health = _data.Health;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) ApplyDamage(1);
        }

        public void ApplyDamage(int damage) => Health -= damage;

        public void ApplyHeal(int heal) => Health += heal;

        private void Die() => gameObject.SetActive(false);
    }
}
