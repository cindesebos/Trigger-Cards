using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/MedKitCardCaster", fileName = "MedKitCardAbilityCaster", order = 0)]
    public class MedKitCardAbilityCaster : CardAbilityCaster
    {
        public override void Cast()
        {
            if(EntitiesObserver.GetCharacter().TryGetComponent(out CharacterHealth characterHealth)) characterHealth.ApplyMaxHeal();
        }
    }
}
