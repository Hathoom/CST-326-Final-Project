using UnityEngine;

namespace Enemy
{
    public class RudimentaryBullet : MonoBehaviour
    {
        public float speed;

        private void Update()
        {
            var myTransform = transform;
            myTransform.position += (myTransform.forward * (speed * Time.deltaTime));
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            Destroy(gameObject);
        }
    }
}
