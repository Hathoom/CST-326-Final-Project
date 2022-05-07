using UnityEngine;

namespace Player
{
    public class MoveCamera : MonoBehaviour
    {


        public Transform targetToFollow;
        public Vector3 offset = Vector3.zero;
        public Vector2 limits = new(5, 3);
        [Range(0,1)] public float followTime;
    
        private Vector3 _velocity = Vector3.zero;

        private void Update()
        {
            if (!Application.isPlaying)
            {
                transform.localPosition = offset;
            }
            
            Move();
        }

        private void LateUpdate()
        {
            var localPos = transform.localPosition;
            transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limits.x, limits.x), 
                Mathf.Clamp(localPos.y, -limits.y, limits.y), localPos.z);
        }

        private void Move()
        {
            /*Debug.Log(targetToFollow.localPosition);*/
            var localPos = transform.localPosition;
            var targetLocalPos = targetToFollow.localPosition;
            transform.localPosition = Vector3.SmoothDamp(localPos, 
                new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, localPos.z), 
                ref _velocity, followTime);
        }
    }
}
