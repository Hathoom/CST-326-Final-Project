using Player;
using UnityEngine;

namespace Power_Up
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] private float healthGain;
        [SerializeField] private bool fullHeal;

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerManager>();
            if (player == null) return;
            var health = fullHeal ? player.maxHealth : healthGain;
            player.GainHealth(health);
            Destroy(gameObject);
        }
    }
}
