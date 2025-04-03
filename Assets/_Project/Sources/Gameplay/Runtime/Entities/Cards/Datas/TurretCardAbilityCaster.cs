using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/TurretCardCaster", fileName = "TurretCardAbilityCaster", order = 0)]
    public class TurretCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _maxBulletSpeed;
        [SerializeField] private float _shotCooldown;
        [SerializeField] private Turret _turret;
        [SerializeField] private SpriteRenderer _visualCastPrefab;
        [SerializeField] private Sprite _visualCastSprite;
        [SerializeField] private Sprite _shotRadiusSprite;

        private SpriteRenderer _visualCastlSlot;
        private SpriteRenderer _shotRadiusSlot;
        private Camera _camera;

        public override void Init(IEntitiesObserver entitiesObserver)
        {
            base.Init(entitiesObserver);
            _camera = Camera.main;
        }

        public override void Cast()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            Turret turret = Instantiate(_turret, mousePosition, Quaternion.identity);

            turret.Init(_damage, _maxBulletSpeed, _shotCooldown);
        }

        public override void SetVisualCastDisplay(bool state)
        {
            if(state)
            {
                if(_visualCastlSlot == null) _visualCastlSlot = Instantiate(_visualCastPrefab);
                if(_shotRadiusSlot == null) _shotRadiusSlot = Instantiate(_visualCastPrefab);

                Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                _visualCastlSlot.transform.position = mousePosition;
                _visualCastlSlot.sprite = _visualCastSprite;

                float spriteHeight = _shotRadiusSlot.bounds.size.y;

                _shotRadiusSlot.transform.position = mousePosition + Vector2.up * (spriteHeight / 2);
                _shotRadiusSlot.sprite = _shotRadiusSprite;
            }
            else
            {
                Destroy(_visualCastlSlot);
                Destroy(_shotRadiusSlot);
            }
        }
    }
}