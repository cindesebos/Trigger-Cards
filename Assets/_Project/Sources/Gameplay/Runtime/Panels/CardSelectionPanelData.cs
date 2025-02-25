using UnityEngine;
using System.Collections.Generic;

namespace Sources.Gameplay.Runtime.Panels
{
    [CreateAssetMenu(menuName = "Sources/Datas/CardSelectionPanelData", fileName = "CardSelectionPanelData", order = 0)]
    public class CardSelectionPanelData : ScriptableObject
    {
        [field: SerializeField] private List<CardRarityData> _cardsRarityData = new List<CardRarityData>();

        public IEnumerable<CardRarityData> CardsRarityData => _cardsRarityData;
    }

    [System.Serializable]
    public class CardRarityData
    {
        public string CategoryName;
        public Rarity Rarity;
        public int Probability;
    }
}