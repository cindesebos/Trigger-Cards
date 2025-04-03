using UnityEngine;
using Zenject;
using System.Collections;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    public class LaserView : Effect
    {
        [SerializeField] private GameObject _children;

        private Laser _laser;

        public void Init(Laser laser)
        {
            _children.SetActive(false);
            _laser = laser;

            _laser.StartedWorking += OnStartWorking;
        }

        private void OnDestroy() => _laser.StartedWorking -= OnStartWorking;

        private void OnStartWorking()
        {
            _children.SetActive(true);
        }
    }
}
