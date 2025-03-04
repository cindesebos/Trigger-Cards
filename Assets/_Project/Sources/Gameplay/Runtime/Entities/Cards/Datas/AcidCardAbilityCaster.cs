using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/AcidCardCaster", fileName = "AcidCardAbilityCaster", order = 0)]
    public class AcidCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private Acid _prefab;
        [SerializeField] private int _damage;
        [SerializeField, Range(1, 10)] private int _maxNumberOfDrops, _minNumberOfDrops;
        [SerializeField] private float _radius;
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
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            for(int i=0; i< GetRandomNumberOfDrops(); i++)
            {
                Acid acid = Instantiate(_prefab, mousePosition, Quaternion.identity);

                acid.Init(_damage, GetRandowPositionInRadius(mousePosition));
            }
        }

        private int GetRandomNumberOfDrops() => Random.Range(_minNumberOfDrops, _maxNumberOfDrops);

        private Vector2 GetRandowPositionInRadius(Vector2 mousePosition)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distance = Random.Range(0f, _radius);

            return mousePosition + new Vector2(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance);
        }

        public override void SetVisualCastDisplay(bool state)
        {
            if(state)
            {
                if(_visualCastlSlot == null) _visualCastlSlot = Instantiate(_visualCastPrefab);

                Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                _visualCastlSlot.transform.position = mousePosition;
                _visualCastlSlot.sprite = _visualCastSprite;
            }
            else Destroy(_visualCastlSlot);
        }
    }
}