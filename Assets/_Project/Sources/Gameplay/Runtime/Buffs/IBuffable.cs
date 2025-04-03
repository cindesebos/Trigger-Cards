using UnityEngine;

namespace Sources.Gameplay.Runtime.Buffs
{
    public interface IBuffable
    {
        void AddBuff(Buff buff);

        void RemoveBuff(Buff buff);
    }
}
