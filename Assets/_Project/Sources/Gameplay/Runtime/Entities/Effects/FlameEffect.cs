using UnityEngine;
using Zenject;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Flame : Effect
    {
        private const float LifeTime = 16f;

        [SerializeField] private Fire[] _fires;

        private int _damage;
        private float _radius;
        private float _rotationSpeed;

        private Transform _target;

        public void Init(int damage, Transform target, float radius, float rotationSpeed)
        {
            _damage = damage;
            _target = target;
            _radius = radius;
            _rotationSpeed = rotationSpeed;

            foreach(Fire fire in _fires) fire.Init(_damage);

            Invoke(nameof(Hide), LifeTime);
        }

        private void Update() => Rotate();

        private void Rotate()
        {
            if(_fires.Length == 0 || _target == null) return;

            float timeOffset = Time.time * _rotationSpeed;

            for(int i = 0; i < _fires.Length; i++)
            {
                float angle = timeOffset + (i * (2 * Mathf.PI / _fires.Length));
                float x = _target.position.x + Mathf.Cos(angle) * _radius;
                float y = _target.position.y + Mathf.Sin(angle) * _radius;
                
                _fires[i].transform.position = new Vector3(x, y, _fires[i].transform.position.z);
                _fires[i].transform.rotation = Quaternion.identity;
            }
        }

        private void Hide() => gameObject.SetActive(false);
    }
}
