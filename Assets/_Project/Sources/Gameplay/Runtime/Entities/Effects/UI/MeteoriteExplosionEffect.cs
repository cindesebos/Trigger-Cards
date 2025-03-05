using UnityEngine;
using System.Collections;

namespace Sources.Gameplay.Runtime.Entities
{
    public class MeteoriteExplosionEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _view;

        private void OnEnable() => Meteorite.Exploded += OnExploded;

        private void OnDisable() => Meteorite.Exploded -= OnExploded;

        private void OnExploded() => _view.SetActive(true);
    }
}
