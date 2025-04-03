using UnityEngine;
using UnityEngine.UI;

namespace Sources.Gameplay.Runtime.Entities
{
    [CreateAssetMenu(menuName = "Sources/Datas/Character", fileName = "CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public Sprite WholeHeart { get; private set; }
        [field: SerializeField] public Sprite BrokenHeart { get; private set; }
    }
}