using UnityEngine;

namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour, IEnemy
    {
        [HideInInspector] public GameObject trackedObject;
        [HideInInspector] public GameObject groupParent;

        [HideInInspector] public string currentState;
        [HideInInspector] public float health;
        [HideInInspector] public int score;

        // bullet stuff
        [HideInInspector] public GameObject bulletPrefab;
        [HideInInspector] public float bulletSpeed, bulletDamage;
        [HideInInspector] public float fireRate, fireRateOffset;
        [HideInInspector] public float minTargetDistance, maxTargetDistance;
        private float _fireTimer;
    
        // swarming stuff
        [HideInInspector] public float swarmRadius, rotationSpeed;
        [HideInInspector] public Vector3 rotationAxis;

        private void Start()
        {
            fireRate += fireRateOffset;
            _fireTimer = Time.time;
            var center = groupParent.transform.position;
            var transformPosition = (Random.insideUnitSphere - center).normalized * swarmRadius + center;
            transform.position = transformPosition;
        }

        private void Update()
        {
            var thisPosition = transform.position;
        
            // swarm around group parent
            var center = groupParent.transform.position;
            transform.RotateAround(center, rotationAxis, rotationSpeed * Time.deltaTime);
            var desiredPosition = (thisPosition - center).normalized * swarmRadius + center;
            thisPosition = desiredPosition;

            // check for range when pathing
            var targetPosition = trackedObject.transform.position;
            var toTarget = targetPosition - thisPosition;
            var distanceToTarget = toTarget.magnitude;
            var dot = Vector3.Dot(targetPosition, toTarget);
        
            if (currentState == "Pathing" && (distanceToTarget < minTargetDistance || distanceToTarget > maxTargetDistance || dot < 0))
            { 
                transform.LookAt(groupParent.transform.position + groupParent.transform.forward);
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
                    groupParent.transform.position,
                    localTransform.rotation).GetComponent<EnemyBullet>();
                bullet.speed = bulletSpeed;
                bullet.damage = bulletDamage;
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
