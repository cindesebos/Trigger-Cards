using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/FlameCardCaster", fileName = "FlameCardAbilityCaster", order = 0)]
    public class FlameCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private float _rotationSpeed = 2f;
        [SerializeField] private Flame _effect;
        [SerializeField] private SpriteRenderer _visualCastPrefab;
        [SerializeField] private Sprite _visualCastSprite;

        private SpriteRenderer _visualCastlSlot;

        public override void Cast()
        {
            Flame flame = Instantiate(_effect);

            flame.Init(_damage, EntitiesObserver.GetCharacter().Transform, _radius, _rotationSpeed);
        }

        public override void SetVisualCastDisplay(bool state)
        {
            if(state)
            {
                if(_visualCastlSlot == null) _visualCastlSlot = Instantiate(_visualCastPrefab);
                
                _visualCastlSlot.transform.position = EntitiesObserver.GetCharacter().Transform.position;
                _visualCastlSlot.sprite = _visualCastSprite;
                _visualCastlSlot.flipX = EntitiesObserver.GetCharacter().GetComponentInChildren<SpriteRenderer>().flipX;
            }
            else Destroy(_visualCastlSlot);
        }
    }
}
