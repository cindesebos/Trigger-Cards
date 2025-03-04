using UnityEngine;

namespace Sources.Utils
{
    public class RadiusDrawer : MonoBehaviour
    {
        [SerializeField] private float _radius;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
