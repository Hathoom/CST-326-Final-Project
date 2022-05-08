using Player;
using UnityEngine;

namespace Power_Up
{
    public class StapleUpgrade : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerCombat>();
            if (player == null) return;
            player.UpgradeWeapon();
            Destroy(gameObject);
        }
    }
}
