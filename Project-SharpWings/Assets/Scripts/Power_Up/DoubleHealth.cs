using Player;
using UnityEngine;

namespace Power_Up
{
    public class DoubleHealth : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerManager>();
            if (player == null) return;
            player.GainMaxHealth(player.maxHealth);
            Destroy(gameObject);
        }
    }
}
