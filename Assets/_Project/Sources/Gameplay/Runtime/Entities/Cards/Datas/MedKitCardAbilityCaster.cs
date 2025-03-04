using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/MedKitCardCaster", fileName = "MedKitCardAbilityCaster", order = 0)]
    public class MedKitCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private int _addHealth = 1;
        [SerializeField] private SpriteRenderer _visualCastPrefab;
        [SerializeField] private Sprite _visualCastSprite;

        private SpriteRenderer _visualCastlSlot;

        public override void Cast()
        {
            if(EntitiesObserver.GetCharacter().TryGetComponent(out CharacterHealth characterHealth)) characterHealth.ApplyMaxHealAddMaxHealth(_addHealth);
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
