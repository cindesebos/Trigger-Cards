using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Entity : MonoBehaviour
    {
        public int Id { get; private set; }
        public Transform Transform => transform;

        private static int _freeId = 0;

        private void Awake()
        {
            Id = _freeId;
            _freeId++;
        }
    }
}