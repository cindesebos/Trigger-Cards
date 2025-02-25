using UnityEngine;
using System.Collections.Generic;

namespace Sources.Gameplay.Runtime.Entities
{
    public class EntitiesObserver : IEntitiesObserver
    {
        public static int FreeId = _freeId;
        private static int _freeId = 0;

        private readonly Dictionary<int, Entity> _entities = new Dictionary<int, Entity>();

        public void Add(Entity entity)
        {
            Debug.Log($"[EntitiesObserver] : was tried to add entity with name: {entity.gameObject.name} and with id {entity.Id}");
            if(entity == null || _entities.ContainsKey(entity.Id)) return;

            _entities.Add(entity.Id, entity);
            _freeId++;

            Debug.Log($"[EntitiesObserver] : was added entity with name: {entity.gameObject.name} and with id {entity.Id}");
        }

        public void Remove(Entity entity)
        {
            if(entity == null || !_entities.ContainsKey(entity.Id)) return;

            _entities.Remove(entity.Id);
            _freeId--;
        }

        public void Remove(int id)
        {
            if(!_entities.ContainsKey(id)) return;

            _entities.Remove(id);
            _freeId--;
        }

        public Entity GetById(int id)
        {
            return _entities.TryGetValue(id, out var entity) ? entity : null;
        }

        public Character GetCharacter()
        {
            foreach(Entity entity in _entities.Values)
            {
                if(entity is Character character) return character;
            }

            return null;
        }

        public IEnumerable<Enemy> GetAllEnemies()
        {
            List<Enemy> enemies = new List<Enemy>();

            foreach(Entity entity in _entities.Values)
            {
                if(entity is Enemy enemy) enemies.Add(enemy);
            }

            return enemies;
        }
    }
}