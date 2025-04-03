using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.Gameplay.Runtime.Panels
{
    public class CardSelectionPanelVFX
    {
        private readonly CardSelectionPanelData _data;
        private readonly CanvasGroup _canvasGroup;
        private readonly Transform[] _cardsSelectionButtonTransforms;
        private readonly Transform[] _cardsFollowPoints;

        public CardSelectionPanelVFX(CardSelectionPanelData data, CanvasGroup canvasGroup, CardSelectionButton[] cardsSelectionButtons, Transform[] cardsFollowPoints)
        {
            _data = data;
            _canvasGroup = canvasGroup;

            _cardsSelectionButtonTransforms = new Transform[cardsSelectionButtons.Length];

            for (int i = 0; i < cardsSelectionButtons.Length; i++) _cardsSelectionButtonTransforms[i] = cardsSelectionButtons[i].transform;

            _cardsFollowPoints = cardsFollowPoints;
        }

        public async UniTask OnShow()
        {
            _canvasGroup.alpha = 0f;

            _canvasGroup.DOFade(1f, _data.FadeDuration).AsyncWaitForCompletion();

            for(int i=0; i<_cardsSelectionButtonTransforms.Length; i++)
            {
                _cardsSelectionButtonTransforms[i].DOMove(_cardsFollowPoints[i].transform.position, _data.CardsMoveDuration).SetEase(Ease.OutBack);
            }
        }

        public async UniTask OnHide()
        {
            _canvasGroup.alpha = 1f;

            await _canvasGroup.DOFade(0f, _data.FadeDuration).AsyncWaitForCompletion();
        }
        
    }
}
