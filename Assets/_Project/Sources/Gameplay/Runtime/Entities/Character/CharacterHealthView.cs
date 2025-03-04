using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Sources.Gameplay.Runtime.Entities
{
    public class CharacterHealthView : MonoBehaviour
    {
        [SerializeField] private CharacterData _data;
        [SerializeField] private Transform _heartsParent;
        [SerializeField] private Image _heartPrefab;

        private List<Image> _hearts = new List<Image>();

        private CharacterHealth _health;
        private Sprite _wholeHeart;
        private Sprite _brokenHeart;

        public void Init(CharacterHealth health)
        {
            _health = health;

            _wholeHeart = _data.WholeHeart;
            _brokenHeart = _data.BrokenHeart;

            InitHearts();

            _health.HealthChanged += OnHealthChanged;
        }

        private void CreateHearts()
        {
            for(int i=0; i<_data.Health; i++) _hearts.Add(Instantiate(_heartPrefab, _heartsParent));
        }

        private void InitHearts()
        {
            foreach(Image heart in _hearts) 
            {
                heart.sprite = _wholeHeart;
            }
        }

        private void OnDestroy() => _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged(int currentHealth)
        {
            if(currentHealth > _hearts.Count)
            {
                int healthToCreate = currentHealth - _hearts.Count;

                for(int h=0; h<healthToCreate; h++) _hearts.Add(Instantiate(_heartPrefab, _heartsParent));
            }

            for(int i=0; i<_hearts.Count; i++)
            {
                if(i < currentHealth) _hearts[i].sprite = _wholeHeart;
                else _hearts[i].sprite = _brokenHeart;
            }
        }
    }
}
