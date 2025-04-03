using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Enemy", fileName = "EnemyData", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
    }
}