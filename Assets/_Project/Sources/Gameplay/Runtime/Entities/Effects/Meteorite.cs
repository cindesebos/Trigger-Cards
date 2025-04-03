using UnityEngine;
using System.Collections;
using System;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Meteorite : Effect
    {
        public static event Action Exploded;

        private const float Delay = 2f;

        private float _radius;
        private int _damage;
        private Vector3 _targetPosition;

        public void Init(float radius, int damage, Vector3 targetPosition)
        {
            _radius = radius;
            _damage = damage;
            _targetPosition = targetPosition;

            StartCoroutine(Move());
            
            Invoke(nameof(Explode), Delay);
        }

        private IEnumerator Move()
        {
            Vector2 startPosition = transform.position;
            float time = 0f;

            while (time < 1f)
            {
                time += Time.deltaTime / Delay;

                Vector2 newPosition = Vector2.Lerp(startPosition, _targetPosition, time);

                transform.position = newPosition;
                yield return null;
            }

            transform.position = _targetPosition;
        }

        private void Explode()
        {
            Exploded?.Invoke();

            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _radius);
            Debug.Log(targets.Length);

            foreach(Collider2D target in targets)
            {
                Debug.Log(target);
                if(target.TryGetComponent(out IDamageable health))
                {
                    if(health is CharacterHealth characterHealth) health.ApplyDamage(1);
                    else health.ApplyDamage(_damage);
                }
            }

            Hide();
        }

        private void Hide() => gameObject.SetActive(false);
    }
}
