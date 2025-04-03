using UnityEngine;
using Zenject;
using Sources.Gameplay.Runtime.Entities;

namespace Sources.Gameplay.Runtime
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _characterSpawnPoint;
        [SerializeField] private Transform _enemySpawnPoint;
        [SerializeField] private CharacterHealthView _characterHealthView;

        private IEntitiesSpawner _entitiesSpawner;

        [Inject]
        private void Construct(IEntitiesSpawner entitiesSpawner)
        {
            Debug.Log(entitiesSpawner);

            _entitiesSpawner = entitiesSpawner;
            Character character = _entitiesSpawner.SpawnCharacter(_characterPrefab, _characterSpawnPoint.position);
            _entitiesSpawner.SpawnEnemy(_enemyPrefab, _enemySpawnPoint.position);

            _characterHealthView.Init(character.GetComponent<CharacterHealth>());
        }
    }
}
