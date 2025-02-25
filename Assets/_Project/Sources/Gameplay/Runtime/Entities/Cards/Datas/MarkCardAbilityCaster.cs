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
        [SerializeField] private MarkDebuff _debuff;

        private Camera _camera;

        public override void Init(IEntitiesObserver entitiesObserver)
        {
            base.Init(entitiesObserver);
            _camera = Camera.main;
        }

        public override void Cast()
        {
            List<Enemy> enemies = EntitiesObserver.GetAllEnemies().ToList();
            Enemy closestEnemy = GetClosestEnemyToMouse(enemies);

            Debug.Log("Was found the closest enemy " + closestEnemy + " the whole list of enemies " + enemies.Count);

            if(closestEnemy) closestEnemy.AddBuff(_debuff);
        }

        private Enemy GetClosestEnemyToMouse(List<Enemy> enemies)
        {
            Debug.Log("closest enemy");
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            return enemies
                .OrderBy(enemy => Vector2.Distance(mousePosition, enemy.transform.position))
                .FirstOrDefault();
        }
    }
}
