using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    public class FlameEffect : Effect
    {
        [SerializeField] private Transform[] flames;
        private float _radius = 2f;
        private float _rotationSpeed = 2f;

        private Transform _target;

        public void Init(Transform target, float radius, float rotationSpeed)
        {
            _target = target;
            _radius = radius;
            _rotationSpeed = rotationSpeed;
        }

        private void Update()
        {
            if (flames.Length == 0 || _target == null) return;

            float timeOffset = Time.time * _rotationSpeed;

            for (int i = 0; i < flames.Length; i++)
            {
                float angle = timeOffset + (i * (2 * Mathf.PI / flames.Length));
                float x = _target.position.x + Mathf.Cos(angle) * _radius;
                float y = _target.position.y + Mathf.Sin(angle) * _radius;
                
                flames[i].position = new Vector3(x, y, flames[i].position.z);
                flames[i].rotation = Quaternion.identity;
            }
        }
    }
}
