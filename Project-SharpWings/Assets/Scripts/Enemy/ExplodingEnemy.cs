using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Enemy
{
    public class ExplodingEnemy : MonoBehaviour, IEnemy
    {
        [Header("General")]
        [SerializeField] private GameObject trackedObject;
        [SerializeField] private List<MeshRenderer> meshes;
        [SerializeField] private float health;
        [SerializeField] private int score;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxTargetingDistance, maxExplodeDistance;
        [SerializeField] private float explosionRadius, explosionGrowthSpeed, explosionDamage;
        [SerializeField] private float explosionLingerTiming;

        
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
            if (_currentState != "Exploding")
            {
                Explode();
            }
            else
            {
                var player = other.gameObject.GetComponent<PlayerManager>();
                player?.TakeDamage(explosionDamage);
                _explosionCollider.enabled = false;
            }
        }
        
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                // Die
                Destroy(gameObject);
            }
        }

        public float GetHealth() => health;
        
        public int GetScore() => score;

        public void SetTarget(GameObject target) => trackedObject = target;
    }
}
