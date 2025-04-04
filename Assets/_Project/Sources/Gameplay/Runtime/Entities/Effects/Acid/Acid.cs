using UnityEngine;
using System.Collections;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Acid : MonoBehaviour
    {
        private const float LifeTime = 8f;

        [SerializeField] private float _jumpTime = 0.25f;
        [SerializeField] private float _jumpHeight = 1f;

        private AcidView _view;
        private int _damege;
        private bool _isActive = false;

        public void Init(int damage, Vector2 targetPosition)
        {
            _isActive = false;
            _damege = damage;
            _view = GetComponentInChildren<AcidView>();

            _view.SetRandomSkin();

            StartCoroutine(JumpCoroutine(targetPosition));

            Invoke(nameof(Hide), LifeTime);
        }

        private IEnumerator JumpCoroutine(Vector2 targetPosition)
        {
            Vector2 startPosition = transform.position;
            float time = 0f;

            while (time < 1f)
            {
                time += Time.deltaTime / _jumpTime;
                float heightOffset = _jumpHeight * Mathf.Sin(Mathf.PI * time);

                Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, time);
                newPosition.y += heightOffset;

                transform.position = newPosition;
                yield return null;
            }

            transform.position = targetPosition;
            _isActive = true;
        }

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!_isActive) return;

            if(other.gameObject.TryGetComponent(out EnemyHealth enemyHealth)) enemyHealth.ApplyDamage(_damege);
        }

        private void Hide() => gameObject.SetActive(false);
    }
}
