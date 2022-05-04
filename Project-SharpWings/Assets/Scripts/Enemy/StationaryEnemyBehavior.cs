using UnityEngine;

namespace Enemy
{
    public class StationaryEnemyBehavior : MonoBehaviour, IEnemy
    {
        public GameObject trackedObject;
        public float health;
        public int score;
        
        public GameObject bulletPrefab;
        public float bulletSpeed;
    
        public float fireRate;
        private float _fireTimer;
        public float minTargetDistance, maxTargetDistance;

        private void Start()
        {
            _fireTimer = Time.time;
        }

        private void Update()
        {
            var thisPosition = transform.position;
            // check for range when pathing
            var targetPosition = trackedObject.transform.position;
            var toTarget = targetPosition - thisPosition;
            var distanceToTarget = toTarget.magnitude;
        
            if (distanceToTarget < minTargetDistance || distanceToTarget > maxTargetDistance)
            {
                transform.LookAt(Vector3.up);
                _fireTimer = Time.time;
                return;
            }
        
        
            // look at player
            transform.LookAt(targetPosition);

            // fire
            if (Time.time - _fireTimer > fireRate)
            {
                _fireTimer = Time.time;
                var localTransform = transform;
                var bullet = Instantiate(bulletPrefab,
                    transform.position + transform.forward,
                    localTransform.rotation).GetComponent<EnemyBullet>();
                bullet.speed = bulletSpeed;
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
