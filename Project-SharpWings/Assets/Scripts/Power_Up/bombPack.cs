using Player;
using UnityEngine;

namespace Power_Up
{
    public class BombPack : MonoBehaviour
    {
        [SerializeField] private int bombs = 1;

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerCombat>();
            if (player == null) return;
            player.AddBombCount(bombs);
            Destroy(gameObject);
        }
    }
}
