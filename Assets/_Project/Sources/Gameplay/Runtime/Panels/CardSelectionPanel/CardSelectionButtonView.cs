using UnityEngine;
using UnityEngine.UI;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime.Panels
{
    public class CardSelectionButtonView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _selectionMark;

        private CardSelectionButton _button;
        private CanvasGroup _canvasGroup;

        public void Init(CardSelectionButton button, Sprite cardIcon, CanvasGroup canvasGroup)
        {
            _icon.sprite = cardIcon;
            _button = button;
            _canvasGroup = canvasGroup;

            _button.Clicked += OnClicked;
        }

        private void OnDisable() => _button.Clicked -= OnClicked;

        public void OnClicked(bool isSelected)
        {
            _canvasGroup.alpha = isSelected ? 0.5f : 1f;

            _selectionMark.SetActive(isSelected);
        }

        public void Reset()
        {
            _canvasGroup.alpha = 1f;
            _selectionMark.SetActive(false);
        }
    }
}
