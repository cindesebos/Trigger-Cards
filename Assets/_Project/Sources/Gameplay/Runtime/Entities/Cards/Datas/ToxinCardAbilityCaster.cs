using UnityEngine;
using Zenject;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/ToxinCardCaster", fileName = "ToxinCardAbilityCaster", order = 0)]
    public class ToxinCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private Toxin _effect;
        [SerializeField] private ToxinDebuff _debuff;
        [SerializeField] private float _moveSpeed;
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

            Toxin toxin = Instantiate(_effect, EntitiesObserver.GetCharacter().Transform.position, Quaternion.Euler(0f, 0f, angle));

            toxin.Init(_moveSpeed, _debuff);
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