using UnityEngine;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Fire : MonoBehaviour
    {
        private int _damege;

        public void Init(int damege) => _damege = damege;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.ApplyDamage(_damege);
            }
        }
    }
}
