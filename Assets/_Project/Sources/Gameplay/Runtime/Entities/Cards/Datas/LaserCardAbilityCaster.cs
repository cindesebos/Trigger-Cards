using UnityEngine;
using Zenject;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/LaserCardCaster", fileName = "LaserCardAbilityCaster", order = 0)]
    public class LaserCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _duration;
        [SerializeField] private int _damage;
        [SerializeField] private float _damageInterval;
        [SerializeField] private Laser _laser;
        [SerializeField] private SpriteRenderer _visualCastPrefab;
        [SerializeField] private Sprite _visualCastSprite;

        private SpriteRenderer _visualCastlSlot;

        private Camera _camera;

        public override void Init(IEntitiesObserver entitiesObserver)
        {
            base.Init(entitiesObserver);
            _camera = Camera.main;
        }

        public override void Cast()
        {
            Vector3 direction = _camera.ScreenToWorldPoint(Input.mousePosition) - EntitiesObserver.GetCharacter().Transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Laser laser = Instantiate(_laser, EntitiesObserver.GetCharacter().Transform.position, Quaternion.Euler(0f, 0f, angle));

            laser.Init(_delay, _duration, _damage, _damageInterval, EntitiesObserver.GetCharacter());
        }

        public override void SetVisualCastDisplay(bool state)
        {
            if(state)
            {
                if(_visualCastlSlot == null) _visualCastlSlot = Instantiate(_visualCastPrefab);

                Vector3 direction = _camera.ScreenToWorldPoint(Input.mousePosition) - EntitiesObserver.GetCharacter().Transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
                _visualCastlSlot.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
                Vector3 characterPosition = EntitiesObserver.GetCharacter().Transform.position;
                _visualCastlSlot.transform.position = new Vector3(characterPosition.x, characterPosition.y, characterPosition.z);
                _visualCastlSlot.sprite = _visualCastSprite;
            }
            else Destroy(_visualCastlSlot);
        }
    }
}