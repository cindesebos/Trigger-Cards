using UnityEngine;
using System;
using System.Collections;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Laser : Effect
    {
        public event Action StartedWorking;

        [SerializeField] private LaserView _view;

        private float _delay;
        private float _duration;
        private int _damage;
        private float _damageInterval;
        private Character _character;
        private bool _canAttack;

        private void OnValidate() => _view = GetComponentInChildren<LaserView>();

        public void Init(float delay, float duration, int damage, float damageInterval, Character character)
        {
            _canAttack = false;
            _view.Init(this);
            _delay = delay;
            _duration = duration;
            _damage = damage;
            _damageInterval = damageInterval;
            _character = character;

            _character.SetLaserShootingState(true);

            Invoke(nameof(StartWorking), _delay);
        }

        private void StartWorking()
        {
            StartedWorking?.Invoke();
            _canAttack = true;

            Invoke(nameof(StopWorking), _duration);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(!_canAttack) return;

            if(other.gameObject.TryGetComponent(out EnemyHealth health))
            {
                _canAttack = false;

                health.ApplyDamage(_damage);

                StartCoroutine(StartAttackCooldown());
            }
        }

        private IEnumerator StartAttackCooldown()
        {
            yield return new WaitForSeconds(_damageInterval);

            _canAttack = true;
        }

        private void StopWorking()
        {
            _canAttack = false;
            _character.SetLaserShootingState(false);

            gameObject.SetActive(false);
        }
    }
}
