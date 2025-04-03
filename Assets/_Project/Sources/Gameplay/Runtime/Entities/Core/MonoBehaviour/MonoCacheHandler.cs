using UnityEngine;
using Zenject;
using System.Collections.Generic;

namespace Sources.Gameplay.Runtime.Entities
{
    public class MonoCacheHandler : ITickable, IFixedTickable
    {
        private readonly List<MonoCache> _entities = new List<MonoCache>(1001);

        public void Register(MonoCache entity) => _entities.Add(entity);

        public void Unregister(MonoCache entity) => _entities.Remove(entity);

        public void Tick()
        {
            for(int i=0; i<_entities.Count; i++) _entities[i].Tick();
        }

        public void FixedTick()
        {
            for(int i=0; i<_entities.Count; i++) _entities[i].FixedTick();
        }
    }
}