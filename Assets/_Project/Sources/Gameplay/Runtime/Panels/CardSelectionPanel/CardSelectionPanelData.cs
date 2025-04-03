using UnityEngine;
using System.Collections.Generic;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime.Panels
{
    [CreateAssetMenu(menuName = "Sources/Datas/CardSelectionPanelData", fileName = "CardSelectionPanelData", order = 0)]
    public class CardSelectionPanelData : ScriptableObject
    {
        [field: SerializeField] public int MaxSelectedItems { get; private set; }
        [field: SerializeField] public float FadeDuration { get; private set; }
        [field: SerializeField] public float CardsMoveDuration { get; private set; }
        [field: SerializeField] private List<CardRarityData> _cardsRarityData = new List<CardRarityData>();

        public IEnumerable<CardRarityData> CardsRarityData => _cardsRarityData;
    }

    [System.Serializable]
    public class CardRarityData
    {
        [field: SerializeField] public string CategoryName { get; private set; }
        [field: SerializeField] public Rarity Rarity { get; private set; }
        [field: SerializeField] private List<CardData> _cards = new List<CardData>();
        [field: SerializeField] public int Probability { get; private set; }
        [field: SerializeField] public Sprite UnknownCardIcon { get; private set; }

        public IEnumerable<CardData> Cards => _cards;
    }
}