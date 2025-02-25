using UnityEngine;
using Zenject;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    public class ToxinEffect : Effect
    {
        private const float LifeTime = 2.5f;

        private float _moveSpeed;
        private ToxinDebuff _debuff;
        private Rigidbody2D _rigidbody2D;

        public void Init(float moveSpeed, ToxinDebuff debuff)
        {
            _moveSpeed = moveSpeed;
            _debuff = debuff;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _rigidbody2D.linearVelocity = transform.right * _moveSpeed;
            Invoke(nameof(Hide), LifeTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.AddBuff(_debuff);
                Hide();
            }
        }

        private void Hide() => gameObject.SetActive(false);
    }
}
