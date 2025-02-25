using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    public class EntitiesSpawner : IEntitiesSpawner
    {
        private readonly DiContainer _diContainer;
        private readonly IEntitiesObserver _observer;

        [Inject]
        public EntitiesSpawner(DiContainer diContainer, IEntitiesObserver observer)
        {
            _diContainer = diContainer;
            _observer = observer;
        }

        public Character SpawnCharacter(Character prefab, Vector3 spawnPosition, Transform parent = null)
        {
            var instance = _diContainer.InstantiatePrefab(prefab, spawnPosition, Quaternion.identity, parent);

            Character character = instance.GetComponent<Character>();

            _observer.Add(character);

            _diContainer.BindInstance(character).AsSingle();

            return character;
        }

        public Enemy SpawnEnemy(Enemy prefab, Vector3 spawnPosition, Transform parent = null)
        {
            var instance = _diContainer.InstantiatePrefab(prefab, spawnPosition, Quaternion.identity, parent);

            Enemy enemy = instance.GetComponent<Enemy>();

            _observer.Add(enemy);

            return enemy;
        }

        public Card SpawnCard(Card prefab, CardData cardData, Transform parent)
        {
            var instance = _diContainer.InstantiatePrefab(prefab, parent);

            Card card = instance.GetComponent<Card>();

            card.Init(cardData);

            return card;
        }
    }
}