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
    }
}