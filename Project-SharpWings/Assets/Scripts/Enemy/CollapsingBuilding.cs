using UnityEngine;

namespace Enemy
{
    public class CollapsingBuilding : MonoBehaviour
    {
        public Vector3 collapseDirection;
        public Vector3 forcePosition;
    
        private Rigidbody _rigidbody;
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    
        public void StartCollapseSequence()
        {
            _rigidbody.AddForceAtPosition(collapseDirection, transform.position + forcePosition);
        }
    }
}
