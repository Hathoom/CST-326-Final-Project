using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Enemy
{
    public class ExplodingEnemy : MonoBehaviour
    {
        [SerializeField] private GameObject trackedObject;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxTargetingDistance, maxExplodeDistance;
        [SerializeField] private float explosionRadius, explosionGrowthSpeed;
        [SerializeField] private float explosionLingerTiming;
        [SerializeField] private List<MeshRenderer> meshes;

        private Transform _transform;
        private SphereCollider _explosionCollider;
        private CapsuleCollider _capsuleCollider;
        private string _currentState;
        private float _explosionTimer;
        
        private void Awake()
        {
            _transform = transform;
            _explosionCollider = GetComponent<SphereCollider>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
        }

        private void Start()
        {
            _explosionCollider.enabled = false;
        }

        private void Update()
        {
            if (trackedObject == null) return;

            var targetDistance = (trackedObject.transform.position - _transform.position).magnitude;
        
            if (targetDistance < maxTargetingDistance) BeginTracking(null);
        
            if (targetDistance < maxExplodeDistance) Explode();

            switch (_currentState)
            {
                case "Tracking":
                    var position = trackedObject.transform.position;
                    _transform.position = Vector3.Lerp(_transform.position, 
                        position,
                        moveSpeed * Time.deltaTime);
                    _transform.LookAt(position);
                    break;
                case "Exploding":
                    if (_explosionCollider.radius < explosionRadius)
                    {
                        _explosionCollider.radius += explosionGrowthSpeed * Time.deltaTime;
                        _explosionTimer = Time.time;
                        return;
                    }

                    if (Time.time - _explosionTimer > explosionLingerTiming)
                    {
                        Destroy(gameObject); // can be replaced with coroutine
                    }
                    break;
            }
        }

        public void BeginTracking(GameObject target)
        {
            if (target != null) trackedObject = target; 
            _currentState = "Tracking";
        }

        public void Explode()
        {
            foreach (var meshRenderer in meshes)
            {
                meshRenderer.enabled = false;
            }
            _capsuleCollider.enabled = false;
            _explosionCollider.enabled = true;
            _currentState = "Exploding";
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("EXPLOSION!");
        }
    }
}
