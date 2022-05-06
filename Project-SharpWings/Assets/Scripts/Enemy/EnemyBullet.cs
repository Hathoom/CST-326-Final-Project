using UnityEngine;

namespace Enemy
{
    public class EnemyBullet : MonoBehaviour
    {
        public float speed;
        public float damage;

        private void Update()
        {
            var myTransform = transform;
            myTransform.position += (myTransform.forward * (speed * Time.deltaTime));
        }

        private void OnCollisionStay(Collision collision)
        {
            var player = collision.gameObject.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            else
            {
                // whiff explode
            }
            
            Destroy(gameObject);
        }
    }
}
