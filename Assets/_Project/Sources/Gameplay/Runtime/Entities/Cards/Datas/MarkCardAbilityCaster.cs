using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Zenject;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/MarkCardCaster", fileName = "MarkCardAbilityCaster", order = 0)]
    public class MarkCardAbilityCaster : CardAbilityCaster
    {
        private const float AdditionalPositionYToSpawn = 1f;

        [SerializeField] private MarkDebuff _debuff;
        [SerializeField] private SpriteRenderer _visualCastPrefab;
        [SerializeField] private Sprite _visualCastSprite;

        private SpriteRenderer _visualCastlSlot;
        private Camera _camera;
        private Enemy _closestEnemy;

        public override void Init(IEntitiesObserver entitiesObserver)
        {
            base.Init(entitiesObserver);
            _camera = Camera.main;
        }

        public override void Cast()
        {
            if(_closestEnemy == null)
            {
                List<Enemy> enemies = EntitiesObserver.GetAllEnemies().ToList();
                _closestEnemy = GetClosestEnemyToMouse(enemies);
            }

            if(_closestEnemy) _closestEnemy.AddBuff(_debuff);
        }

        private Enemy GetClosestEnemyToMouse(List<Enemy> enemies)
        {
            Debug.Log("closest enemy");
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            return enemies
                .OrderBy(enemy => Vector2.Distance(mousePosition, enemy.transform.position))
                .FirstOrDefault();
        }

        public override void SetVisualCastDisplay(bool state)
        {
            if(state)
            {
                if(_visualCastlSlot == null) _visualCastlSlot = Instantiate(_visualCastPrefab);

                List<Enemy> enemies = EntitiesObserver.GetAllEnemies().ToList();
                _closestEnemy =  GetClosestEnemyToMouse(enemies);

                Vector3 enemyPosition = _closestEnemy.Transform.position;
                enemyPosition.y += AdditionalPositionYToSpawn;
                _visualCastlSlot.transform.position = enemyPosition;
                _visualCastlSlot.sprite = _visualCastSprite;
            }
            else Destroy(_visualCastlSlot);
        }
    }
}
