using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    public class MonoCache : MonoBehaviour, IUpdateable, IFixedUpdateable
    {
        private MonoCacheHandler _handler;

        public void Init(MonoCacheHandler handler)
        {
            Debug.Log("Injected object with name " + gameObject.name);
            _handler = handler;
        }

        private void OnEnable() => _handler.Register(this);

        private void OnDisable() => _handler.Unregister(this);

        public void Tick() { }

        public void FixedTick() { }
    }
}