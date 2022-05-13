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
        [HideInInspector] public float delay;
        [HideInInspector] public float linger;
        private float _effectTimer;
        private float _explosionTimer;

        public delegate void EnemyDeath(int score);
        public event EnemyDeath OnEnemyDeath;

        private BoxCollider _boxedCollider;
        private SphereCollider _effectCollider;

        public ParticleSystem explosionEShockWave;
        public ParticleSystem explosionEEmber;
        public ParticleSystem explosionESmoke;

        public List<MeshRenderer> meshList;

        private bool _isExploded;

        private void Start()
        {
            _effectTimer = 5;
            
            _boxedCollider = GetComponent<BoxCollider>();
            _effectCollider = GetComponent<SphereCollider>();
            _explosionTimer = Time.time;

            _effectCollider.enabled = false;
            _isExploded = false;
        }

        private void Update()
        {
            if (!_isExploded) transform.position += direction * (speed * Time.deltaTime);
            
            if (Time.time - _explosionTimer > delay)
            {
                Explode();

                explosionEShockWave.Play();
                explosionEEmber.Play();
                explosionESmoke.Play();
            }
            else
            {
                _effectTimer = Time.time;
            }

            if (_isExploded && _effectCollider.radius < radius) _effectCollider.radius += radius;
            
            if (_isExploded && Time.time - _effectTimer > linger)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var enemy = collision.gameObject.GetComponent<IEnemy>();
            if (enemy == null) return;
            enemy.TakeDamage(damage);
            if(enemy.GetHealth() <= 0 ) OnEnemyDeath?.Invoke(enemy.GetScore());
        }

        private void Explode()
        {
            foreach (var meshL in meshList)
            {
                meshL.enabled = false;
            }

            _boxedCollider.enabled = false;
            _effectCollider.enabled = true;
            
            _isExploded = true;
        }

    }
}
