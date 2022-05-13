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
        public Transform barrel;
        public AudioSource AS;

        //death animation
        private bool isDead;

        private float timer;
        private Rigidbody rBody;

        private Animator animator;

        public GameObject Explosion;

        private void Start()
        {
            bulletSpeed = 100;
            AS = GetComponent<AudioSource>();
            _fireTimer = Time.time;
            
            rBody = gameObject.GetComponent<Rigidbody>();
            animator = gameObject.GetComponent<Animator>();

            timer = 0f;
        }

        private void Update()
        {
            if (!isDead)
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
                    AS.Play();
                    _fireTimer = Time.time;
                    var localTransform = barrel;
                    var bullet = Instantiate(bulletPrefab,
                        localTransform.position + localTransform.forward,
                        bulletPrefab.transform.rotation * localTransform.rotation).GetComponent<EnemyBullet>();
                    bullet.speed = bulletSpeed;
                    bullet.direction = localTransform.forward;
                    bullet.lifeTime = 5f;
                    bullet.damage = 10;
                }
            }

            timer = timer + Time.deltaTime;
        }
        
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {  
                //player shoots them a second time
                if (isDead)
                {
                    Instantiate(Explosion, transform.position, transform.rotation);

                    Destroy(gameObject);
                }


                // tells the animator to go to an empty animation
                animator.enabled = false;

                // make the rbody have gravity so that it will fall.
                rBody.useGravity = true;
                rBody.isKinematic = false;

                // create a random torque that the enemy be forced to spin with.
                var v = Random.insideUnitSphere;
                v *= Random.value > 0.5 ? -360 : 360;
                rBody.AddTorque(v, ForceMode.Impulse);

                // let the rest of the enemy know that the enemy is dead.
                isDead = true;
            }
        }

        public float GetHealth() => health;
        public int GetScore() => score;
        
        public void SetTarget(GameObject target) => trackedObject = target;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerDamage")) return;
            // when the enemy has died, if the enemy collides with something it dies.
            if (isDead)
            {
                Instantiate(Explosion, transform.position, transform.rotation);

                Destroy(gameObject);
            }
            
        }
    }
}
