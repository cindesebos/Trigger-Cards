using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/TapeCardCaster", fileName = "TapeCardAbilityCaster", order = 0)]
    public class TapeCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private int _healthAmount;

        public override void Cast()
        {
            if(EntitiesObserver.GetCharacter().TryGetComponent(out CharacterHealth characterHealth)) characterHealth.ApplyHeal(_healthAmount);
        }
    }
}
