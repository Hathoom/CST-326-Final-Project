using UnityEngine;
using UnityEngine.Events;

public class TriggerEnemies : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject, bool> triggerEvent;
    [SerializeField] private bool autoGeneratePath = true;

    private void OnTriggerEnter(Collider other)
    {
        triggerEvent?.Invoke(other.gameObject, autoGeneratePath);
        Debug.Log("bitchin");
    }
}
