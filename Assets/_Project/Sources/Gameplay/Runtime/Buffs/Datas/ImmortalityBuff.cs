using UnityEngine;
using UniRx;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime.Buffs
{
    [CreateAssetMenu(menuName = "Sources/Datas/Buffs/Immortality", fileName = "ImmortalityBuff", order = 0)]
    public class ImmortalityBuff : Buff
    {
        [SerializeField] private float _duration;

        public override void Apply(Entity entity)
        {
            Character character = entity as Character;

            if (character == null) return;

            character.SetImmortalityState(true);

            Observable.Timer(System.TimeSpan.FromSeconds(_duration))
                .Subscribe(_ =>
                {
                    character.RemoveBuff(this);
                })
                .AddTo(character);
        }

        public override void Remove(Entity entity)
        {
            Character character = entity as Character;

            if (character == null) return;

            character.SetImmortalityState(false);
        }
    }
}