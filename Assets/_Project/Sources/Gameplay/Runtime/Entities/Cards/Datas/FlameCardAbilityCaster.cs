using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/FlameCardCaster", fileName = "FlameCardAbilityCaster", order = 0)]
    public class FlameCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private float _rotationSpeed = 2f;
        [SerializeField] private Flame _effect;

        public override void Cast()
        {
            Flame flame = Instantiate(_effect);

            flame.Init(_damage, EntitiesObserver.GetCharacter().Transform, _radius, _rotationSpeed);
        }
    }
}
