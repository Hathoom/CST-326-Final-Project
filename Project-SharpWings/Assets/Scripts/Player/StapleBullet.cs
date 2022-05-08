using Enemy;
using UnityEngine;

namespace Player
{
    public class StapleBullet : MonoBehaviour
    {
        [HideInInspector] public Vector3 direction;
        [HideInInspector] public float speed;
        [HideInInspector] public float damage;
        [HideInInspector] public float lifeTime;
        private float _seconds;
        private float _deathTimer;

        public delegate void EnemyDeath(int score);
        public event Bullet.EnemyDeath OnEnemyDeath;
    
        private void Start()
        {
            _deathTimer = Time.time;
        }

        private void Update()
        {
            transform.position += direction * speed * Time.deltaTime;

            if (Time.time - _deathTimer > lifeTime) Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var enemy = collision.gameObject.GetComponent<IEnemy
            >();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                if (enemy.GetHealth() <= 0) OnEnemyDeath?.Invoke(enemy.GetScore());
            }
            Destroy(gameObject);
        }
    }
}
