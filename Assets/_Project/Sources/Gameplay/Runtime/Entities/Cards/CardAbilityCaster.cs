using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public abstract class CardAbilityCaster : ScriptableObject
    {
        protected IEntitiesObserver EntitiesObserver;

        public virtual void Init(IEntitiesObserver entitiesObserver) => EntitiesObserver = entitiesObserver;

        public virtual void Cast() { }

        public virtual void SetVisualCastDisplay(bool state) { }
    }
}