using UnityEngine;
using System;

namespace Sources.Gameplay.Runtime.Entities
{
    public class PlasmaView : Effect
    {
        private const float LightningLifeTime = 0.1f;

        [SerializeField] private Sprite[] _skins;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject _lightningPrefab;
        [SerializeField] private float _spawnLightningRange;

        private Plasma _plasma;

        private void OnValidate() => _spriteRenderer ??= GetComponent<SpriteRenderer>();

        public void Init(Plasma plasma)
        {
            _plasma = plasma;

            if(_skins.Length != 0) SetRandomSkin();

            _plasma.Shot += OnShot;
        }

        private void OnDestroy() => _plasma.Shot -= OnShot;

        private void SetRandomSkin() => _spriteRenderer.sprite = _skins[UnityEngine.Random.Range(0, _skins.Length)];

        private void OnShot(Enemy enemy)
        {
            Vector3 directionToEnemy = (enemy.Transform.position - _plasma.transform.position).normalized;
            Vector3 spawnPosition = transform.position + directionToEnemy * _spawnLightningRange;

            GameObject lightning = Instantiate(_lightningPrefab, spawnPosition, Quaternion.identity);

            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
            lightning.transform.rotation = Quaternion.Euler(0, 0, angle);

            Destroy(lightning, LightningLifeTime);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _spawnLightningRange);
        }
    }
}
