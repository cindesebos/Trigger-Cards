using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/TapeCardCaster", fileName = "TapeCardAbilityCaster", order = 0)]
    public class TapeCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private int _healthAmount;
        [SerializeField] private SpriteRenderer _visualCastPrefab;
        [SerializeField] private Sprite _visualCastSprite;

        private SpriteRenderer _visualCastlSlot;

        public override void Cast()
        {
            if(EntitiesObserver.GetCharacter().TryGetComponent(out CharacterHealth characterHealth)) characterHealth.ApplyHeal(_healthAmount);
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
