using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/SpikesCardCaster", fileName = "SpikesCardAbilityCaster", order = 0)]
    public class SpikesCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _damage;
        [SerializeField] private Spikes _effect;
        [SerializeField] private SpriteRenderer _visualCastPrefab;
        [SerializeField] private Sprite _visualCastSprite;

        private SpriteRenderer _visualCastlSlot;

        public override void Cast()
        {
            Spikes spikes = Instantiate(_effect);

            spikes.Init(EntitiesObserver.GetCharacter().Transform, _moveSpeed, _damage);
        }

        public override void SetVisualCastDisplay(bool state)
        {
            if(state)
            {
                if(_visualCastlSlot == null) _visualCastlSlot = Instantiate(_visualCastPrefab);
                _visualCastlSlot.transform.position = EntitiesObserver.GetCharacter().Transform.position;
                _visualCastlSlot.sprite = _visualCastSprite;
            }
            else Destroy(_visualCastlSlot);
        }
    }
}
