using UnityEngine;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime.Buffs
{
    public abstract class Buff : ScriptableObject
    {
        public virtual void Apply(Entity entity) { }

        public virtual void Remove(Entity entity) { }
    }
}
