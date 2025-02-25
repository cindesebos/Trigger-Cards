using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/SpikesCardCaster", fileName = "SpikesCardAbilityCaster", order = 0)]
    public class SpikesCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _damage;
        [SerializeField] private SpikesEffect _effect;

        public override void Cast()
        {
            SpikesEffect spikesEffect = Instantiate(_effect);

            spikesEffect.Init(EntitiesObserver.GetCharacter().Transform, _moveSpeed, _damage);
        }
    }
}
