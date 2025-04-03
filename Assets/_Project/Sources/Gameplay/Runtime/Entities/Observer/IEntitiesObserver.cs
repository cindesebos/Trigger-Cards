using UnityEngine;
using System.Collections.Generic;

namespace Sources.Gameplay.Runtime.Entities
{
    public interface IEntitiesObserver
    {
        void Add(Entity entity);

        void Remove(int id);

        void Remove(Entity entity);

        Entity GetById(int id);

        Character GetCharacter();

        IEnumerable<Enemy> GetAllEnemies();
        
        void SetFreezeStateAllEntities(bool state);
    }
}