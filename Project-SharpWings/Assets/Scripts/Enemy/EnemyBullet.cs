using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyBullet : MonoBehaviour
    {
        [HideInInspector] public Vector3 direction;
        [HideInInspector] public float speed;
        [HideInInspector] public float damage;
        [HideInInspector] public float lifeTime;
        private float _seconds;
        private float _deathTimer;

        private void Start()
        {
            _deathTimer = Time.time;
        }

        void Update()
        {
            transform.position += direction * speed * Time.deltaTime;

            if (Time.time - _deathTimer > lifeTime) Destroy(gameObject);
        }


        private void OnCollisionStay(Collision collision)
        {
            var player = collision.gameObject.GetComponent<PlayerManager>();
            if (player != null)
            {            
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
