using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardMovement _movement;
        [SerializeField] private Image _icon;

        private CardData _data;
        private IEntitiesObserver _entitiesObserver;

        private void OnValidate()
        {
            _movement ??= GetComponent<CardMovement>();
            _icon ??= GetComponent<Image>();
        }

        [Inject]
        private void Construct(IEntitiesObserver entitiesObserver)
        {
            _entitiesObserver = entitiesObserver;
        }

        public void Init(CardData data)
        {
            _data = data;

            _icon.sprite = _data.Icon;
            _movement.Init(_data);
            _data.AbilityCaster.Init(_entitiesObserver);
        }
    }
}