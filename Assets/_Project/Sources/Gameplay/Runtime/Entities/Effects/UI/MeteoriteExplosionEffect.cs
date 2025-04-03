using UnityEngine;
using System.Collections;

namespace Sources.Gameplay.Runtime.Entities
{
    public class MeteoriteExplosionEffect : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private void OnEnable() => Meteorite.Exploded += OnExploded;

        private void OnDisable() => Meteorite.Exploded -= OnExploded;

        private void OnExploded() => _animator.SetTrigger("appeared");
    }
}
