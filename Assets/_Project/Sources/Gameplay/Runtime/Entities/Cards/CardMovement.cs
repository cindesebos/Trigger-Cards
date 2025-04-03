using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Sources.Gameplay.Runtime.Entities
{
    public class CardMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private const float MinRangeOfUse = 1f;

        private CardData _data;

        private HorizontalLayoutGroup _horizontalLayoutGroup;
        private CanvasGroup _canvasGroup;
        private Camera _camera;
        private Vector3 _offSet;
        private Vector3 _startPosition;
        private Transform _defaultParent;
        private int _originalSiblingIndex;
        private bool _isMovingCard;

        public void Init(CardData data)
        {
            _data = data;
            _camera = Camera.main;
            _defaultParent = transform.parent;
            _horizontalLayoutGroup = GetComponentInParent<HorizontalLayoutGroup>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _originalSiblingIndex = transform.GetSiblingIndex();
            _horizontalLayoutGroup.enabled = false;
            transform.SetParent(_defaultParent.parent);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _horizontalLayoutGroup.enabled = true;
            transform.SetParent(_defaultParent);
            transform.SetSiblingIndex(_originalSiblingIndex);

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isMovingCard = true;
            _offSet = transform.position - _camera.ScreenToWorldPoint(eventData.position);
            _offSet.z = 0f;

            _startPosition = transform.position;
        }

        private void Update()
        {
            if(_isMovingCard)  _data.AbilityCaster.SetVisualCastDisplay(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 0.05f;
            Vector3 mousePosition = _camera.ScreenToWorldPoint(eventData.position);
            mousePosition.z = 0f;          
            transform.position = mousePosition + _offSet;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isMovingCard = false;
            float distance = Vector3.Distance(transform.position, _startPosition);

            if(distance > MinRangeOfUse)
            {
                _horizontalLayoutGroup.enabled = true;
                _data.AbilityCaster.Cast();
                Destroy(gameObject);
            }
            else
            {
                transform.position = _startPosition;
                _canvasGroup.alpha = 10f;
            }

            _data.AbilityCaster.SetVisualCastDisplay(false);
        }
    }
}
