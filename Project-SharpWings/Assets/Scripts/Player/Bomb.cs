using Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Bomb : MonoBehaviour
    {
        [HideInInspector] public Vector3 direction;
        [HideInInspector] public float speed;
        [HideInInspector] public float damage;
        [HideInInspector] public float radius;
        [HideInInspector]  public float force;
        [HideInInspector] public float delay;
        [HideInInspector] private float effectTimer;
        //[HideInInspector] private float inLargeBY;

        private float _explosionTimer;
        public GameObject explosionEffect;

        public delegate void EnemyDeath(int score);
        public event EnemyDeath OnEnemyDeath;

        public BoxCollider _boxedCollider;
        public SphereCollider _effectCollider;

        //These will be for the small projectile bombs around the sphere

        public GameObject _fragment1;
        public SphereCollider _fragment2;
        public SphereCollider _fragment3;
        public SphereCollider _fragment4;

        
        public ParticleSystem explosionE_ShockWave;
        public ParticleSystem explosionE_Ember;
        public ParticleSystem explosionE_Smoke;


        public Transform _transform;

        public List<MeshRenderer> meshList;

        private string _eventOfExplosion;


        private void Start()
        {
            delay = 2f;
            effectTimer = 5;
            //inLargeBY = 30;
            damage = 700;
            //_transform = transform;
            
            _boxedCollider = GetComponent<BoxCollider>();
            _effectCollider = GetComponent<SphereCollider>();
            _explosionTimer = Time.time;
            //fragment colliders
            //_fragment1 = GetComponent<Rigidbody>();
            
            //_fragment2 = GetComponent<Rigidbody>();
            //_fragment3 = GetComponent<Rigidbody>();
            //_fragment4 = GetComponent<Rigidbody>();

            _effectCollider.enabled = false;
        }

        private void Update()
        {
            transform.position += direction * (speed * Time.deltaTime);

            

            if (Time.time - _explosionTimer > delay)
            {
                Explode();

                explosionE_ShockWave.Play();
                explosionE_Ember.Play();
                explosionE_Smoke.Play();
            }


            switch (_eventOfExplosion)
            {
                case "Effect":

                    // with in the effect I want the smaller bombs to scater into different areas
                   // _fragment1.r.position += Time.deltaTime * 10f;

                   


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

          //Debug.Log("Hit Something" + collision.gameObject.name);

           var connection = collision.gameObject.GetComponent<IEnemy>();

        
          // Debug.Log("Enemy:  ---  "+ damage  + " : " + connection.GetHealth()    );
            if(connection != null){
                //Debug.Log(collision.gameObject.name + " " + collision);
                connection.TakeDamage(damage);
                if(connection.GetHealth() <= 0 ) OnEnemyDeath?.Invoke(connection.GetScore());
            }
            

        

            //Destroy(gameObject);
        }

        private void Explode()
        {
            //shows effect
            //Instantiate(explosionEffect, transform);
            //Debug.Log("Explosion");

            foreach (var meshL in meshList)
            {
                meshL.enabled = false;
            }

            _boxedCollider.enabled = false;
            _effectCollider.enabled = true;

            // // get nearby objects
            // var colliders = Physics.OverlapSphere(transform.position, radius);

            // foreach (var nearbyObject in colliders)
            // {
            //     //add force
            
            //     var rb = nearbyObject.GetComponent<Rigidbody>();
            //     if (rb != null)
            //     {
            //         rb.AddExplosionForce(force, Vector3.forward * 10, radius);
            //     }

            // }

            _eventOfExplosion = "Effect";



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
