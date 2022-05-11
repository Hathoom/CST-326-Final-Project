using System;
using Enemy;
using TMPro;
using UnityEngine;

namespace Player
{
    public class StapleBullet : MonoBehaviour
    {
        [HideInInspector] public Vector3 direction;
        [HideInInspector] public float speed;
        [HideInInspector] public float damage;
        private float _seconds;
        private bool _isTravelling;
        
        public delegate void EnemyDeath(int score);
        public event Bullet.EnemyDeath OnEnemyDeath;

        private void Start()
        {
            _isTravelling = true;
        }

        private void Update()
        {
            if (_isTravelling) transform.position += direction * speed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var enemy = collision.gameObject.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                if (enemy.GetHealth() <= 0)
                {
                    OnEnemyDeath?.Invoke(enemy.GetScore());
                }
            }
            else
            {
                _isTravelling = false;
            }
        }
    }
}
