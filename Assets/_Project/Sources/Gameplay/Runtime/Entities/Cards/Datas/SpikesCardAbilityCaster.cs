using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/SpikesCardCaster", fileName = "SpikesCardAbilityCaster", order = 0)]
    public class SpikesCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _damage;
        [SerializeField] private Spikes _effect;

        public override void Cast()
        {
            Spikes spikes = Instantiate(_effect);

            spikes.Init(EntitiesObserver.GetCharacter().Transform, _moveSpeed, _damage);
        }
    }
}
