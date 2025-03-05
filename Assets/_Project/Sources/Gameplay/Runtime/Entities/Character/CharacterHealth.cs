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
                _health = Mathf.Clamp(value, 0, _maxHealth);

                HealthChanged?.Invoke(_health);

                Debug.Log("now health is " + _health);

                if(_health <= 0) Die();
            }
        }

        private int _health;
        private int _maxHealth;
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
            Debug.Log("apply damage: " + damage + " immorality state is " + _character.IsImmortality());
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
