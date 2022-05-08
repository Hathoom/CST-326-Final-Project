using System.Collections;
using Player;
using UnityEngine;

namespace Power_Up
{
    public class BombPack : MonoBehaviour
    {
        [SerializeField] private int bombs = 1;

        private AudioSource _audio;
        private void Awake() => _audio = GetComponent<AudioSource>();
        
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerCombat>();
            if (player == null) return;
            player.AddBombCount(bombs);
            _audio.Play();
            foreach (var render in GetComponentsInChildren<Renderer>())
            {
                render.enabled = false;
            }
            StartCoroutine(PickupEffect());
        }

        private IEnumerator PickupEffect()
        {
            while (_audio.isPlaying) yield return null;
            Destroy(gameObject);
        }
    }
}
