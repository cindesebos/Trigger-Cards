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
                if(value < 0 || value > _maxHealth) return;

                _health = value;

                HealthChanged?.Invoke(_health);

                if(_health <= 0) Die();
            }
        }

        public int _health;
        public int _maxHealth;
        private CharacterData _data;
        private Character _character;

        public void Init(CharacterData data, Character character)
        {
            _data = data;
            _character = character;

            _maxHealth = _data.Health;
            Health = _maxHealth;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) ApplyDamage(1);
        }

        public void ApplyDamage(int damage)
        {
            if(!_character.IsImmortality()) Health -= damage;
        }

        public void ApplyHeal(int heal) => Health += heal;

        public void ApplyMaxHealAddMaxHealth(int newMaxHealth)
        {
            _maxHealth += newMaxHealth;
            Health = _maxHealth;
        }

        private void Die() => gameObject.SetActive(false);
    }
}
