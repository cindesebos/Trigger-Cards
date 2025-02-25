using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public class SpikesEffect : Effect
    {
        [SerializeField] private Spike[] _spikes;

        public void Init(Transform target, float moveSpeed, int damage)
        {
            transform.position = target.position;

            foreach(Spike spike in _spikes) spike.Init(moveSpeed, damage);
        }

        private void Update()
        {
            foreach(Spike spike in _spikes) spike.Move();
        }
    }
}
