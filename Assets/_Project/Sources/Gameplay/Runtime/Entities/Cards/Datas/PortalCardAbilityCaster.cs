using UnityEngine;
using Zenject;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/PortalCardCaster", fileName = "PortalCardAbilityCaster", order = 0)]
    public class PortalCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private Portal _portal;
        [SerializeField] private ImmortalityBuff _buff;
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
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            Portal portal = Instantiate(_portal, EntitiesObserver.GetCharacter().Transform.position, Quaternion.identity);

            portal.Init(EntitiesObserver, mousePosition, _buff);
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