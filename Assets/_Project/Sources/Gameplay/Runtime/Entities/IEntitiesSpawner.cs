using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public interface IEntitiesSpawner
    {
        public Character SpawnCharacter(Character prefab, Vector3 spawnPosition, Transform parent = null);
        public Enemy SpawnEnemy(Enemy prefab, Vector3 spawnPosition, Transform parent = null);
        public Card SpawnCard(Card prefab, CardData data, Transform parent);
    }
}