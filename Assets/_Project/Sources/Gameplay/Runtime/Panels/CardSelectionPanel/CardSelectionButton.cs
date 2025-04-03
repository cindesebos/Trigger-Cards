using UnityEngine;
using UnityEngine.UI;
using System;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime.Panels
{
    public class CardSelectionButton : MonoBehaviour
    {
        public event Action<bool> Clicked;

        public bool IsSelected { get; private set; }
        public CardData SelectedCardData { get; private set; }
        [field: SerializeField] public bool isUnknownCardType { get; private set; } = false;

        private CardSelectionButtonView _view;
        private Button _button;
        private CanvasGroup _canvasGroup;
        private CardSelectionPanel _cardSelectionPanel;

        public void Setup(CardSelectionPanel cardSelectionPanel)
        {
            _cardSelectionPanel = cardSelectionPanel;

            _button = GetComponent<Button>();
            _view = GetComponent<CardSelectionButtonView>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _button.onClick.AddListener( delegate {
                OnClicked();
            });
        }

        public void Init(CardData cardData, Sprite cardIcon)
        {
            SelectedCardData = cardData;

            _view.Init(this, cardIcon, _canvasGroup);
        }

        public void OnClicked()
        {
            Debug.Log($"Clicked on {SelectedCardData} " + IsSelected);

            if(!IsSelected)
            {
                if(!_cardSelectionPanel.CanSelectItem) return;
            }

            IsSelected = !IsSelected;

            Clicked?.Invoke(IsSelected);
            _cardSelectionPanel.OnButtonSelected(this);
        }

        public void OnReset()
        {
            IsSelected = false;
            _view.Reset();
        }
    }
}
