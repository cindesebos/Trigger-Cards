using UnityEngine;
using UnityEngine.UI;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Card", fileName = "CardData", order = 0)]
    public class CardData : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Rarity Rarity { get; private set; }

        [field: SerializeField] public CardAbilityCaster AbilityCaster { get; private set; }
    }
}