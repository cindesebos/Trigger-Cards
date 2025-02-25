using UnityEngine;
using Zenject;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Cards/Abilities/StunCardyCaster", fileName = "StunCardAbilityCaster", order = 0)]
    public class StunCardAbilityCaster : CardAbilityCaster
    {
        [SerializeField] private StunDebuff _debuff;
        [SerializeField] private int _radius;
        [SerializeField] private LayerMask _targetLayer;

        private Camera _camera;

        public override void Init(IEntitiesObserver entitiesObserver)
        {
            base.Init(entitiesObserver);
            _camera = Camera.main;
        }

        public override void Cast()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(mousePosition, _radius, _targetLayer);

            foreach(Collider2D enemyCollider in enemies)
            {
                if(enemyCollider.TryGetComponent(out Enemy enemy)) enemy.AddBuff(_debuff);
            }
        }
    }
}
