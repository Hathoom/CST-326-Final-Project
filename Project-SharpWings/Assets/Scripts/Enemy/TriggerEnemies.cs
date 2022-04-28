using UnityEngine;
using UnityEngine.Events;

public class TriggerEnemies : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> triggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        triggerEvent?.Invoke(other.gameObject);
        Destroy(gameObject);
    }
}
