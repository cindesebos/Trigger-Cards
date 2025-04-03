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
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _enemySpawnPoint;
        [SerializeField] private Button _spawnCardButton;

        private IEntitiesSpawner _entitiesSpawner;
        private CardSelectionPanel _cardSelectionPanel;

        [Inject]
        private void Construct(IEntitiesSpawner entitiesSpawner, CardSelectionPanel cardSelectionPanel) 
        {
            _entitiesSpawner = entitiesSpawner;
            _cardSelectionPanel = cardSelectionPanel;
        }

        public void CreateCard()
        {
            _entitiesSpawner.SpawnCard(_cardPrefab, _cardData, _cardParent);
        }

        public void ShowCardSelectionPanel()
        {
            _cardSelectionPanel.Show();
        }

        public void SpawnEnemy()
        {
            _entitiesSpawner.SpawnEnemy(_enemyPrefab, _enemySpawnPoint.position);
        }
    }
}