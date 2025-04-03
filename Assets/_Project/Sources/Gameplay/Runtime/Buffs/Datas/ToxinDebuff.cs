using UnityEngine;
using UniRx;
using System;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime.Buffs
{
    [CreateAssetMenu(menuName = "Sources/Datas/Buffs/Toxin", fileName = "ToxinDebuff", order = 0)]
    public class ToxinDebuff : Buff
    {
        [SerializeField] private float _duration;
        [SerializeField] private int _damage;
        [SerializeField] private Sprite _icon;

        public override void Apply(Entity entity)
        {
            Enemy enemy = entity as Enemy;

            if(enemy == null) return;

            float currentDuration = _duration;

            IDisposable damageSubscription = Observable.Interval(TimeSpan.FromSeconds(1))
                .TakeWhile(_ => currentDuration >= 0)
                .Subscribe(_ =>
                {
                    currentDuration--;
                    enemy.SetToxinState(true, _icon, _damage);
                },
                () => enemy.SetToxinState(true, _icon));
        }

        public override void Remove(Entity entity)
        {
            Enemy enemy = entity as Enemy;

            if(enemy == null) return;

            enemy.SetToxinState(false, _icon);
        }
    }
}