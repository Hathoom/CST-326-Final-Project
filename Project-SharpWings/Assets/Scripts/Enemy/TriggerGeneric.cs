using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class TriggerGeneric : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> triggerEvent;
        [SerializeField] private bool killSelfAfterTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            triggerEvent?.Invoke(other.gameObject);
            if (killSelfAfterTrigger) Destroy(gameObject);
        }
    }
}
