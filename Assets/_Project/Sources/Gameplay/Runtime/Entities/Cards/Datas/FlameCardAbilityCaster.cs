using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/FlameCardCaster", fileName = "FlameCardAbilityCaster", order = 0)]
    public class FlameCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private FlameEffect _effect;

        public override void Cast()
        {
            FlameEffect flameEffect = Instantiate(_effect);

            flameEffect.Init(EntitiesObserver.GetCharacter().Transform, _radius, _rotationSpeed);
        }
    }
}
