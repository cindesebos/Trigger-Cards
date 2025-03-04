using UnityEngine;
using System;

namespace Sources.Gameplay.Runtime.Entities
{
    public class AcidView : MonoBehaviour
    {
        [SerializeField] private Sprite[] _skins;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void OnValidate() => _spriteRenderer ??= GetComponent<SpriteRenderer>();

        public void SetRandomSkin() => _spriteRenderer.sprite = _skins[UnityEngine.Random.Range(0, _skins.Length)];
    }
}
