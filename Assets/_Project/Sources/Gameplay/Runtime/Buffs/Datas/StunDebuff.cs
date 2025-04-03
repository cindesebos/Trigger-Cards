using UnityEngine;
using UniRx;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime.Buffs
{
    [CreateAssetMenu(menuName = "Sources/Datas/Buffs/Stun", fileName = "StunDebuff", order = 0)]
    public class StunDebuff : Buff
    {
        [SerializeField] private float _duration;
        [SerializeField] private Sprite _icon;

        public override void Apply(Entity entity)
        {
            Enemy enemy = entity as Enemy;

            if (enemy == null) return;

            enemy.SetStunState(true, _icon);

            Observable.Timer(System.TimeSpan.FromSeconds(_duration))
                .Subscribe(_ =>
                {
                    enemy.RemoveBuff(this);
                })
                .AddTo(enemy);
        }

        public override void Remove(Entity entity)
        {
            Enemy enemy = entity as Enemy;

            if (enemy == null) return;

            enemy.SetStunState(false, _icon);
        }
    }
}