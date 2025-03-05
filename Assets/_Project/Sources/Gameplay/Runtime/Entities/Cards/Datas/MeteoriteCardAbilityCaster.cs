using UnityEngine;
using Zenject;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/MeteoriteCardCaster", fileName = "MeteoriteCardAbilityCaster", order = 0)]
    public class MeteoriteCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private float _radius;
        [SerializeField] private int _damage;
        [SerializeField] private Meteorite _meteorite;
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
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 spawnPosition = new Vector2(mousePosition.x, mousePosition.y + 15f);

            Meteorite meteorite = Instantiate(_meteorite, spawnPosition, Quaternion.identity);

            meteorite.Init(_radius, _damage, mousePosition);
        }

        public override void SetVisualCastDisplay(bool state)
        {
            if(state)
            {
                Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                if(_visualCastlSlot == null) _visualCastlSlot = Instantiate(_visualCastPrefab);
                _visualCastlSlot.transform.position = mousePosition;
                _visualCastlSlot.sprite = _visualCastSprite;
            }
            else Destroy(_visualCastlSlot);
        }
    }
}
