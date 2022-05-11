using Enemy;
using UnityEngine;
using System.Collections.Generic;
using Player;

namespace Player
{
    public class Bomb : MonoBehaviour
    {
        [HideInInspector] public Vector3 direction;
        [HideInInspector] public float speed;
        [HideInInspector] public float damage;
        [HideInInspector] public float radius;
        [HideInInspector] public float force;
        [HideInInspector] public float delay;
        [HideInInspector] private float effectTimer;
        [HideInInspector] private float inLargeBY;

        private float _explosionTimer;
        public GameObject explosionEffect;

        public delegate void EnemyDeath(int score);
        public event EnemyDeath OnEnemyDeath;

        public BoxCollider _boxedCollider;
        public SphereCollider _effectCollider;

        public Transform _transform;

        public List<MeshRenderer> meshList;

        private string _eventOfExplosion;


    
        private void Start()
        {
            delay = 2;
            effectTimer = 2;
            inLargeBY = 30;
            //_transform = transform;
            _explosionTimer = Time.time;
            _boxedCollider = GetComponent<BoxCollider>();
            _effectCollider = GetComponent<SphereCollider>();

            _effectCollider.enabled = false;
        }

        private void Update()
        {
            transform.position += direction * (speed * Time.deltaTime);

            if (Time.time - _explosionTimer > delay)
            {
                Explode();
               
            }

            switch (_eventOfExplosion)
            {
                case "Effect":

                    if (_effectCollider.radius < radius)
                    {
                        _effectCollider.radius += inLargeBY * Time.deltaTime;
                        _explosionTimer = Time.time;
                        return;
                    }
                    

                    if (Time.time - _explosionTimer > effectTimer)
                    {
                        Destroy(gameObject);
                    }
                    break;


            }
        }

        private void OnCollisionEnter(Collision collision)
        {
          //Explode();
        }

        private void Explode()
        {
            //shows effect
            Debug.Log("Explosion");

            foreach (var meshL in meshList)
            {
                meshL.enabled = false;
            }

            _boxedCollider.enabled = false;
            _effectCollider.enabled = true;

            _eventOfExplosion = "Effect";

       


            //Instantiate(explosionEffect, transform);

            // var enemy = collision.gameObject.GetComponent<IEnemy>();
            // // make sure that we are colliding with the enemy

            // //shows effect
            // Instantiate(explosionEffect, transform);

            // if(enemy != null)
            // {
            //     //calls the funtion to make the enemy take damage
            //     enemy.TakeDamage(damage);

            //     if(enemy.GetHealth() < 0) OnEnemyDeath?.Invoke(enemy.GetScore());



            // }


            // //removes the bomb and interacts with the bomb
            // Destroy(gameObject);


            /**

            // get nearby objects
            var colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (var nearbyObject in colliders)
            {
                //add force
            
                var rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                //Damage
                var enemy = nearbyObject.GetComponent<IEnemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    if (enemy.GetHealth() <= 0) OnEnemyDeath?.Invoke(enemy.GetScore());
                }
            }

            
            */
            //remove bomb
            // Destroy(gameObject);

        }

    }
}
