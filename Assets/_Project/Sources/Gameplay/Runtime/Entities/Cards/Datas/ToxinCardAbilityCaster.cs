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
    }
}