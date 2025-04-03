using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using Cysharp.Threading.Tasks;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime.Panels
{
    public class CardSelectionPanel : MonoBehaviour
    {
        [field: SerializeField] public bool CanSelectItem { get; private set; } = true;

        [SerializeField] private Card _cardPrefab;
        [SerializeField] private Transform _cardParent;
        [Space]

        [SerializeField] private CardSelectionPanelData _data;
        [SerializeField] private TextMeshProUGUI _itemsCountDisplayer;
        [SerializeField] private CardSelectionButton[] _cardsSelectionButtons;
        [SerializeField] private Transform[] _cardsFollowPoints;
        [SerializeField] private GameObject _children;
        [SerializeField] private Button _applyButton;
        [SerializeField] private CanvasGroup _canvasGroup;

        private List<CardData> _selectedCards = new List<CardData>();
        private int _maxSelectedItems;
        private int _selectedItems;

        private IEntitiesSpawner _entitiesSpawner;
        private IEntitiesObserver _entitiesObserver;
        private CardSelectionPanelVFX _cardSelectionPanelVFX;

        [Inject]
        private void Construct(IEntitiesSpawner entitiesSpawner, IEntitiesObserver entitiesObserver) 
        {
            _entitiesSpawner = entitiesSpawner;
            _entitiesObserver = entitiesObserver;

            _cardSelectionPanelVFX = new CardSelectionPanelVFX(_data, _canvasGroup, _cardsSelectionButtons, _cardsFollowPoints);

            _applyButton.onClick.AddListener(() => Apply().Forget());

            SetupButtons();
        }
        
        public async UniTask Show()
        {
            _entitiesObserver.SetFreezeStateAllEntities(true);
            _children.SetActive(true);
            _selectedCards.Clear();

            await _cardSelectionPanelVFX.OnShow();

            Init();
            SetRandomCards();
        }

        private void Init()
        {
            CanSelectItem = true;
            _maxSelectedItems = _data.MaxSelectedItems;

            _itemsCountDisplayer.text = _selectedItems.ToString() + "/" + _maxSelectedItems.ToString();
        }

        private void SetupButtons()
        {
            foreach(CardSelectionButton cardSelectionButton in _cardsSelectionButtons) cardSelectionButton.Setup(this);
        }

        private void SetRandomCards()
        {
            var rarityList = _data.CardsRarityData.ToList();

            int totalWeight = rarityList.Sum(rarity => rarity.Probability);

            foreach(CardSelectionButton cardSelectionButton in _cardsSelectionButtons)
            {
                CardRarityData selectedRarity = GetRandomRarity(rarityList, totalWeight);

                int radnomRarityIndex = Random.Range(0, rarityList.Count);

                var cardList = selectedRarity.Cards.ToList();

                if(cardList.Count > 0)
                {
                    int randomIndex = Random.Range(0, cardList.Count);
                    if(cardSelectionButton.isUnknownCardType) InitButton(cardSelectionButton, cardList[randomIndex], selectedRarity);
                    else InitButton(cardSelectionButton, cardList[randomIndex]);
                }
            }
        }

        private CardRarityData GetRandomRarity(List<CardRarityData> rarityList, int totalWeight)
        {
            int randomValue = Random.Range(0, totalWeight);
            int currentSum = 0;

            foreach (var rarity in rarityList)
            {
                currentSum += rarity.Probability;

                if(randomValue < currentSum)
                {
                    return rarity;
                }
            }

            return rarityList.Last();
        }

        private void InitButton(CardSelectionButton cardSelectionButton, CardData cardData, CardRarityData selectedRarity = null)
        {
            if(selectedRarity != null)
            {
                cardSelectionButton.Init(cardData, selectedRarity.UnknownCardIcon);

                return;
            }

            cardSelectionButton.Init(cardData, cardData.Icon);
        }

        public void OnButtonSelected(CardSelectionButton cardSelectionButton)
        {
            if(cardSelectionButton.IsSelected)
            {
                _selectedItems++;
                _selectedCards.Add(cardSelectionButton.SelectedCardData);
            }
            else
            {
                _selectedItems--;
                _selectedCards.Remove(cardSelectionButton.SelectedCardData);
            }

            if(_selectedItems == _maxSelectedItems) CanSelectItem = false;
            else CanSelectItem = true;

            _itemsCountDisplayer.text = _selectedItems.ToString() + "/" + _maxSelectedItems.ToString();
        }

        public async UniTask Apply()
        {
            foreach(CardData cardData in _selectedCards)
            {
                _entitiesSpawner.SpawnCard(_cardPrefab, cardData, _cardParent);
            }

            await _cardSelectionPanelVFX.OnHide();

            Hide();

            Reset();

            _entitiesObserver.SetFreezeStateAllEntities(false);
        }

        private void Reset()
        {
            _selectedItems = 0;
            CanSelectItem = true;

            foreach(CardSelectionButton cardSelectionButton in _cardsSelectionButtons)
            {
                cardSelectionButton.OnReset();
            }
        }

        private void Hide() => _children.SetActive(false);
    }
}