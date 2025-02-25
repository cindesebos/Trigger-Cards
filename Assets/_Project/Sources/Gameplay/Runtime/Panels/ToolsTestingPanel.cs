using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime.Panels
{
    public class ToolsTestingPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private CardData _cardData;
        [SerializeField] private Transform _cardParent;
        [SerializeField] private Button _spawnCardButton;

        private IEntitiesSpawner _entitiesSpawner;

        [Inject]
        private void Construct(IEntitiesSpawner entitiesSpawner) 
        {
            _entitiesSpawner = entitiesSpawner;
        }

        public void CreateCard()
        {
            _entitiesSpawner.SpawnCard(_cardPrefab, _cardData, _cardParent);
        }
    }
}