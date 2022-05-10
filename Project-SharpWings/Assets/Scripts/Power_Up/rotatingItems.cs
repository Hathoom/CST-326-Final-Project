using UnityEngine;

namespace Power_Up
{
    public class rotatingItems : MonoBehaviour
    {
        [SerializeField] private Vector3 rotation;

        private void Update()
        {
            transform.Rotate(rotation * Time.deltaTime);
        }
    }
}
