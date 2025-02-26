using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/PlasmaCardCaster", fileName = "PlasmaCardAbilityCaster", order = 0)]
    public class PlasmaCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private Plasma _plasma;
        [SerializeField] private int _damage;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _shotCooldown;
        [SerializeField] private float _shotRange = 2f;

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

            Plasma plasma = Instantiate(_plasma, EntitiesObserver.GetCharacter().Transform.position, Quaternion.Euler(0f, 0f, angle));

            plasma.Init(_damage, _moveSpeed, _shotCooldown, _shotRange, EntitiesObserver);
        }
    }
}