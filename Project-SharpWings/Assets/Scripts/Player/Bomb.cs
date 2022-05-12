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
            Explode();
        }

        private void Explode()
        {
            //shows effect
            //Instantiate(explosionEffect, transform);
           // Debug.Log("Explosion");

            foreach (var meshL in meshList)
            {
                meshL.enabled = false;
            }

            _boxedCollider.enabled = false;
            _effectCollider.enabled = true;

            _eventOfExplosion = "Effect";


        }


    }
}
