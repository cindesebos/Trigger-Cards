using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/DroneCardCaster", fileName = "DroneCardAbilityCaster", order = 0)]
    public class DroneCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _height;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _shotCooldown;
        [SerializeField] private Drone _drone;

        private Camera _camera;

        public override void Init(IEntitiesObserver entitiesObserver)
        {
            base.Init(entitiesObserver);
            _camera = Camera.main;
        }

        public override void Cast()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            Drone drone = Instantiate(_drone, mousePosition, Quaternion.identity);

            drone.Init(_damage, _moveSpeed, _bulletSpeed, _shotCooldown, EntitiesObserver);
        }
    }
}